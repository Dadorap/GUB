using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Common;
using Services.BusinessLogic.LandingPage;
using ViewModels.Infrastructure.Paging;
using ViewModels.ViewModel.Accounts;
using ViewModels.ViewModel.LandingPage;

namespace ViewModels.Pages
{
    public class TestModel : PageModel
    {
        private readonly ITopTenService _topTenService;
        private readonly IMapper _mapper;

        public TestModel(ITopTenService topTenService, IMapper mapper)
        {
            _topTenService = topTenService;
            _mapper = mapper;
        }

        public List<TopTenViewModel> TopTen { get; set; } = new();

        public void OnGet(string countryName)
        {
            var c = _topTenService.GetTopTen("Finland");
            TopTen = _mapper.Map<List<TopTenViewModel>>(c);
                  
        }
    }
}
