using GUB.API;
using Services.BusinessLogic.LandingPage;

namespace GUB.ViewModel.LandingPage
{
    public class CountryCardData : ICountryCardData
    {
        private readonly ICardsInfoService _cardsInfoService;

        public CountryCardData(ICardsInfoService cardsInfoService)
        {

            _cardsInfoService = cardsInfoService;
        }
        public List<LandingPageCardViewModel> GetCountryData(string country)
        {
            if (country == "Sweden")
            {
                return _cardsInfoService.GetCountryList("Sweden").Select(c => new LandingPageCardViewModel
                {
                    Country = c.Country,
                    Accounts = c.Accounts,
                    Balance = c.Balance,
                    Transactions = c.Transactions
                }).ToList();
            }
            else if (country == "Finland")
            {
                return _cardsInfoService.GetCountryList("Finland").Select(c => new LandingPageCardViewModel
                {
                    Country = c.Country,
                    Accounts = c.Accounts,
                    Balance = c.Balance,
                    Transactions = c.Transactions
                }).ToList();
            }
            else if (country == "Denmark")
            {
                return _cardsInfoService.GetCountryList("Denmark").Select(c => new LandingPageCardViewModel
                {
                    Country = c.Country,
                    Accounts = c.Accounts,
                    Balance = c.Balance,
                    Transactions = c.Transactions
                }).ToList();
            }
            else
            {
                return _cardsInfoService.GetCountryList("Norway").Select(c => new LandingPageCardViewModel
                {
                    Country = c.Country,
                    Accounts = c.Accounts,
                    Balance = c.Balance,
                    Transactions = c.Transactions
                }).ToList();
            }
        }
    }
}
