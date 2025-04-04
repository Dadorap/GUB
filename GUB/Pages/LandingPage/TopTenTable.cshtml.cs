using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.LandingPage;
using ViewModels.ViewModel.LandingPage;

namespace GUB.Pages.LandingPage
{
    [ResponseCache(Duration = 30, VaryByQueryKeys = new[] { "countryName" })]
    public class TopTenTableModel : PageModel
    {

        private readonly ITopTenService _topTenService;
        private readonly IMapper _mapper;

        public TopTenTableModel(ITopTenService topTenService, IMapper mapper)
        {
            _topTenService = topTenService;
            _mapper = mapper;
        }

        public string Country { get; set; }
        public List<TopTenViewModel> TopTen { get; set; } = new();

        public void OnGet(string countryName)
        {
            Country = countryName.Trim();
            var c = _topTenService.GetTopTen(countryName);
            TopTen = _mapper.Map<List<TopTenViewModel>>(c);

        }
    }

}
