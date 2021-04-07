using Infra.CrossCutting.Models;
using Infra.CrossCutting.Tools.Models;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Infra.CrossCutting.Tools.Service
{
    public class RsaService : IRsaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ILogModel _log { get; set; }

        public RsaService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogModel log)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient(_configuration["Jwt:RsaServiceHttpClient"]);
            _log = log;
        }

        public async Task<RSAParameters> GetRSAParameters()
        {
            RsaInfo rsaInfo = await GetPublicKeyInfo(_configuration["Jwt:PublicKeyURL"]).ConfigureAwait(false);

            if (string.IsNullOrEmpty(rsaInfo?.Modulus) || string.IsNullOrEmpty(rsaInfo?.Exponent))
            {
                try
                {
                    rsaInfo = new RsaInfo
                    {
                        Exponent = "AQAB",
                        Modulus = "iXG5aKladHDx2zDKu5DOOB0jEuJQsTeR5KlVdb96x9+ZvPiBzd2nRmJiioFEFTcv6YYj1su+m7mV6wocIyYO+J6zZ3qygW+yTcP3FZD05qQMUf1kRUfzKQ7jMZuIPAPWUaBDKqidVh3MPZalAoGUSkQ6vohwXmY1ETkNELZsg4M="
                    };
                }
                catch (Exception ex)
                {
                    _log.RecLog(nameof(GetRSAParameters), $"Error {ex.Message} - {ex.InnerException} {ex.StackTrace}", LogType.LogError);
                }
            }
            try
            {
                var _rsa = new RSACryptoServiceProvider(2048);

                _rsa.ImportParameters(new RSAParameters()
                {
                    Modulus = FromBase64Url(rsaInfo.Modulus),
                    Exponent = FromBase64Url(rsaInfo.Exponent)
                });

                return _rsa.ExportParameters(false);
            }
            catch (Exception ex)
            {
                _log.RecLog(nameof(GetRSAParameters), $"Error {ex.Message} - {ex.InnerException} {ex.StackTrace}", LogType.LogError);
                throw;
            }
        }

        private static byte[] FromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/")
                                  .Replace("-", "+");
            return Convert.FromBase64String(base64);
        }

        private async Task<RsaInfo> GetPublicKeyInfo(string serverKeyUrl)
        {
            if (string.IsNullOrWhiteSpace(serverKeyUrl) || serverKeyUrl == string.Empty)
                return new RsaInfo { Error = "Server key URL is null or empty" };

            try
            {
                var publicKey = await GetPublicKey(serverKeyUrl);

                if (!string.IsNullOrEmpty(publicKey?.Error))
                    return new RsaInfo { Error = "Public key not found in that url" };

                if (!string.IsNullOrEmpty(publicKey?.Value))
                    return ExtractData(publicKey.Value);
                else
                    return new RsaInfo { Error = "Public key is null" };
            }
            catch (Exception ex)
            {
                _log.RecLog(nameof(GetPublicKeyInfo), $"Error url:({serverKeyUrl}) {ex.Message} - {ex.InnerException} {ex.StackTrace}", LogType.LogError);
                return new RsaInfo { Error = ex.Message };
            }
        }

        private async Task<PublicKey> GetPublicKey(string URL)
        {
            var publicKey = new PublicKey();

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, new Uri(URL));
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);

            if (httpResponse.IsSuccessStatusCode)
            {
                publicKey.Value = await httpResponse.Content.ReadAsStringAsync();
            }
            else
            {
                publicKey.Error = $" HttpResponse: {httpResponse.StatusCode} Content: {await httpResponse.Content.ReadAsStringAsync()}";
            }

            return publicKey;
        }

        private RsaInfo ExtractData(string publicKey)
        {
            PemReader pemReader = new PemReader(new StringReader(publicKey));
            AsymmetricKeyParameter keyParameter = (AsymmetricKeyParameter)pemReader.ReadObject();
            RsaKeyParameters rsaParams = (RsaKeyParameters)keyParameter;

            byte[] exponent = rsaParams.Exponent.ToByteArrayUnsigned();
            byte[] modulus = rsaParams.Modulus.ToByteArrayUnsigned();

            return new RsaInfo
            {
                Exponent = Convert.ToBase64String(exponent, 0, exponent.Length),
                Modulus = Convert.ToBase64String(modulus, 0, modulus.Length)
            };
        }
    }
}