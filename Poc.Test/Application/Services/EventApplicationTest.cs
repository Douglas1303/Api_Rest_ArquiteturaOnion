using AutoMapper;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Moq;
using Poc.Application.AutoMapper;
using Poc.Application.Service;
using Poc.Domain.Interface.Repository;

namespace Poc.Test.Application.Services
{
    public class EventApplicationTest
    {
        private readonly Mock<IEventRepository> _mockedEventRepository;
        private readonly Mock<IMediatorHandler> _mockedMediatorHandler;
        private readonly IMapper _mapperFake;
        private readonly Mock<ILogModel> _mockedLog;
        private readonly EventApplication _eventApplication;

        public EventApplicationTest()
        {
            _mockedEventRepository = new Mock<IEventRepository>();
            _mockedMediatorHandler = new Mock<IMediatorHandler>();
            _mockedLog = new Mock<ILogModel>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });

            _mapperFake = new Mapper(config); 

            _eventApplication = new EventApplication(_mockedEventRepository.Object, _mockedMediatorHandler.Object, _mapperFake,
                                                     _mockedLog.Object);
        }
    }
}