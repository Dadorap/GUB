using AutoMapper;
using DataAccessLayer.DTOs;
using ViewModels.ViewModel.Accounts;
using ViewModels.ViewModel.LandingPage;

namespace ViewModels.Infrastructure.Paging
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TransactionDTO, TransactionDetailsViewModel>()
                .ReverseMap();            
            CreateMap<TopTenDTO, TopTenViewModel>()
                .ReverseMap();
        }
    }
}
