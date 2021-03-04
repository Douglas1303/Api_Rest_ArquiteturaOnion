using AutoMapper;
using ExternalServices.Cep.Interface;
using Infra.CrossCutting.Models;
using MediatR;
using Moq;
using Poc.Application.AutoMapper;
using Poc.Application.Service;
using Poc.Domain.Interface.Repository;

namespace Poc.Test.Application.Services
{
    public class SponsorApplicationTest
    {
        private readonly Mock<ISponsorRepository> _mockedSponsorRepository;
        private readonly Mock<IMediator> _mockedMediator;
        private readonly Mock<ILogModel> _mockedLogModel;
        private readonly IMapper _mapperFake;
        private readonly Mock<ICepService> _mockedCepService;
        private readonly SponsorApplication _sponsorApplication;

        public SponsorApplicationTest()
        {
            _mockedSponsorRepository = new Mock<ISponsorRepository>();
            _mockedMediator = new Mock<IMediator>();
            _mockedLogModel = new Mock<ILogModel>();
            _mockedCepService = new Mock<ICepService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });

            _mapperFake = new Mapper(config); 

            _sponsorApplication = new SponsorApplication(_mockedSponsorRepository.Object, _mockedMediator.Object, _mockedLogModel.Object,
                                                         _mapperFake, _mockedCepService.Object);
        }
    }
}