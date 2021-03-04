using AutoMapper;
using ExternalServices.Cep.Interface;
using Moq;
using Poc.Application.AutoMapper;
using Poc.Application.Service;

namespace Poc.Test.Application.Services
{
    public class CepApplicationTest
    {
        private readonly Mock<ICepService> _mockedCepService;
        private readonly IMapper _mapperFake;
        private readonly CepApplication _cepApplication;

        public CepApplicationTest()
        {
            _mockedCepService = new Mock<ICepService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });

            _mapperFake = new Mapper(config);  

            _cepApplication = new CepApplication(_mockedCepService.Object, _mapperFake);
        }
    }
}