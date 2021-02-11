using Infra.CrossCutting.Core.CQRS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Poc.Application.Interface.Identity;
using Poc.Application.Model;
using Poc.Application.ViewModel.Identity;
using Poc.Domain.Interface.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Application.Service.Identity
{
    public class AuthorizationApplication : IAuthorizationApplication
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ICustomUserManagerRepository _customUserManagerRepository; 

        public AuthorizationApplication(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, ICustomUserManagerRepository customUserManagerRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _customUserManagerRepository = customUserManagerRepository; 
        }

        public async Task<IResult> LoginAsync(LoginIdentityViewModel loginViewModel)
        {
            //verificar as credenciais do usuário e retornar um valor
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email,
                loginViewModel.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return new QueryResult(GeraToken(loginViewModel));
            }
            else
            {
                return new QueryResult("Login Inválido ou senha Inválido...");
            }
        }

        public async Task<IResult> RegisterUserAsync(UserIdentityViewModel viewModel)
        {
            var user = new IdentityUser
            {
                UserName = viewModel.Email,
                Email = viewModel.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);

            if (!result.Succeeded)
            {
                return new QueryResult(GetErrors(result));
            }

            await _signInManager.SignInAsync(user, false);
            return new QueryResult("Usuario registrado com sucesso! Já pode fazer o login.");
        }

        private static string GetErrors(IdentityResult result)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var erro in result.Errors)
            {
                sb.AppendFormat("{0}, {1}", erro.Description, Environment.NewLine);
            }

            return sb.ToString();
        }

        private UserTokenModel GeraToken(LoginIdentityViewModel userInfo)
        {
            //definir delarações do usuário
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.NameId, _customUserManagerRepository.GetUserById(userInfo.Email)),
                new Claim("Dev", "Events"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gerar uma chave com base em um algoritmo simetrico
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            //gerar assinatura digital do token usando o algoritmo Hmac e a chave privada
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //tempo de expiração do token
            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            //classe que representa um token JWT e gera o token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credenciais);

            //retornar os dados com o token de informações
            return new UserTokenModel()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT OK"
            };
        }
    }
}