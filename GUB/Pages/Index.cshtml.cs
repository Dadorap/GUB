using DataAccessLayer.DTOs;
using ViewModels.API;
using ViewModels.ViewModel.LandingPage;
using ViewModels.ViewModel.ZenQuotes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using Services.BusinessLogic.LandingPage;
using System.Diagnostics.Metrics;


namespace ViewModels.Pages;

public class IndexModel : PageModel
{
    private readonly ZenQuotesService _zenQuotesService;
    private readonly ICountryCardData _countryCardData;

    public IndexModel(ZenQuotesService zenQuotesService,
                      ICountryCardData countryCardData)
    {
        _zenQuotesService = zenQuotesService;
        _countryCardData = countryCardData;
    }


    public List<ZenQuotesViewModel> ZenQuotes { get; set; } = new();
    public List<LandingPageCardViewModel> CountryCard { get; set; } = new();

    List<string> countries = new() { "Sweden", "Finland", "Norway", "Denmark" };
    public string DateOnly { get; set; }

    public async Task OnGet()
    {
        ZenQuotes = (await _zenQuotesService.GetQuotes())
            .Select(q => new ZenQuotesViewModel
            {
                Quote = q.Quote,
                Author = q.Author
            }).ToList();
        DateTime now = DateTime.Now;
        DateOnly = now.ToString("dddd, MMMM d 'at' HH:mm", CultureInfo.InvariantCulture);


        for (int i = 0; i < countries.Count; i++)
        {
            var data = _countryCardData
                .GetCountryData(countries[i])
                .Select(s => new LandingPageCardViewModel()
            {
                Country = s.Country,
                Customers = s.Customers,
                Balance = s.Balance,
                Accounts = s.Accounts,
            })
                .ToList();

            CountryCard.AddRange(data);
        }
    }
}
