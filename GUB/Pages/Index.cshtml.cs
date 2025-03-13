using DataAccessLayer.DTOs;
using GUB.API;
using GUB.ViewModel.LandingPage;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.LandingPage;
using System.Globalization;

namespace GUB.Pages;

public class IndexModel : PageModel
{
    private readonly ZenQuotesService _zenQuotesService;
    private readonly ICountryCardData _countryCardData;

    public IndexModel(ZenQuotesService zenQuotesService, ICountryCardData countryCardData)
    {
        _zenQuotesService = zenQuotesService;
        _countryCardData = countryCardData;
    }


    public List<ZenQuotesDTO> ZenQuotes { get; set; } = new List<ZenQuotesDTO>();
    public List<LandingPageCardViewModel> SweCard { get; set; } = new();
    public List<LandingPageCardViewModel> FinCard { get; set; } = new();
    public List<LandingPageCardViewModel> NorCard { get; set; } = new();
    public List<LandingPageCardViewModel> DkCard { get; set; } = new();
    public string DateOnly { get; set; }

    public async Task OnGet()
    {
        ZenQuotes = await _zenQuotesService.GetQuotes(); 
        DateTime now = DateTime.Now;
        DateOnly = now.ToString("dddd, MMMM d 'at' HH:mm", CultureInfo.InvariantCulture);

        SweCard = _countryCardData.GetCountryData("Sweden");
        FinCard = _countryCardData.GetCountryData("Finland");
        NorCard = _countryCardData.GetCountryData("Norway");
        DkCard = _countryCardData.GetCountryData("Denmark");


    }
}
