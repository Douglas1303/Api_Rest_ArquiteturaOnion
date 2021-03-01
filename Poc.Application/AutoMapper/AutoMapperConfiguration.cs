using AutoMapper;
using ExternalServices.Cep;
using ExternalServices.Cep.Model;
using Poc.Application.ViewModel;
using Poc.Domain.Dtos;
using Poc.Domain.Entities;

namespace Poc.Application.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            //ModelToViewModel
            CreateMap<CategoryModel, CategoryViewModel>();
            CreateMap<CepModel, CepViewModel>(); 
            CreateMap<EventModel, EventViewModel>();
            CreateMap<UserModel, UserViewModel>(); 

            //DataTransferObjectToViewModel
            CreateMap<SponsorDto, SponsorViewModel>(); 
        }
    }
}