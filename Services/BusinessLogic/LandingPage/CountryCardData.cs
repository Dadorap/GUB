using DataAccessLayer.DTOs;
using Services.BusinessLogic.LandingPage;
using System.Globalization;

namespace Services.BusinessLogic.LandingPage;

public class CountryCardData : ICountryCardData
{
    private readonly ICardsInfoService _cardsInfoService;

    public CountryCardData(ICardsInfoService cardsInfoService)
    {

        _cardsInfoService = cardsInfoService;
    }
    public List<LnadingPargeCardDTO> GetCountryData(string country)
    {
        var countryList = _cardsInfoService.GetCountryList(country);

        return countryList.Select(c => new LnadingPargeCardDTO
        {
            Country = c.Country,
            Customers = c.Customers.ToString("#,##0", CultureInfo.InvariantCulture)
                .Replace(",", " "),
            Balance = c.Balance.ToString("#,##0.00", CultureInfo.InvariantCulture)
                .Replace(",", " ")
                .Replace(".", ","),
            Accounts = c.Accounts.ToString("#,##0", CultureInfo.InvariantCulture)
                .Replace(",", " ")
        }).ToList();

    }
}
