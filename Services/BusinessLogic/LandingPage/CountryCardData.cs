using Contracts.DTOs;
using Services.BusinessLogic.LandingPage;
using System.Globalization;

namespace GUB.ViewModel.LandingPage;

public class CountryCardData : ICountryCardData
{
    private readonly ICardsInfoService _cardsInfoService;

    public CountryCardData(ICardsInfoService cardsInfoService)
    {

        _cardsInfoService = cardsInfoService;
    }
    public List<LnadingPargeCardDTO> GetCountryData(string country)
    {
        if (country == "Sweden")
        {
            return _cardsInfoService.GetCountryList("Sweden").Select(c => new LandingPageCardViewModel
            {
                Country = c.Country,
                Customers = c.Customers.ToString("#,##0", CultureInfo.InvariantCulture)
                .Replace(",", " "),
                Balance = c.Balance.ToString("#,##0.00", CultureInfo.InvariantCulture)
                .Replace(",", " ")
                .Replace(".", ","),
                Accounts = c.Transactions.ToString("#,##0", CultureInfo.InvariantCulture)
                .Replace(",", " ")
            }).ToList();
        }
        else if (country == "Finland")
        {
            return _cardsInfoService.GetCountryList("Finland").Select(c => new LandingPageCardViewModel
            {
                Country = c.Country,
                Customers = c.Customers.ToString("#,##0", CultureInfo.InvariantCulture)
                .Replace(",", " "),
                Balance = c.Balance.ToString("#,##0.00", CultureInfo.InvariantCulture)
                .Replace(",", " ")
                .Replace(".", ","),
                Accounts = c.Transactions.ToString("#,##0", CultureInfo.InvariantCulture)
                .Replace(",", " ")
            }).ToList();
        }
        else if (country == "Denmark")
        {
            return _cardsInfoService.GetCountryList("Denmark").Select(c => new LandingPageCardViewModel
            {
                Country = c.Country,
                Customers = c.Customers.ToString("#,##0", CultureInfo.InvariantCulture)
                .Replace(",", " "),
                Balance = c.Balance.ToString("#,##0.00", CultureInfo.InvariantCulture)
                .Replace(",", " ")
                .Replace(".", ","),
                Accounts = c.Transactions.ToString("#,##0", CultureInfo.InvariantCulture)
                .Replace(",", " ")
            }).ToList();
        }
        else
        {
            return _cardsInfoService.GetCountryList("Norway").Select(c => new LandingPageCardViewModel
            {
                Country = c.Country,
                Customers = c.Customers.ToString("#,##0", CultureInfo.InvariantCulture)
                .Replace(",", " "),
                Balance = c.Balance.ToString("#,##0.00", CultureInfo.InvariantCulture)
                .Replace(",", " ")
                .Replace(".", ","),
                Accounts = c.Transactions.ToString("#,##0", CultureInfo.InvariantCulture)
                .Replace(",", " ")
            }).ToList();
        }
    }
}
