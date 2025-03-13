using DataAccessLayer.DTOs;

namespace Services.BusinessLogic.LandingPage;

public interface ICardsInfoService
{
    List<CountryDTO> GetCountryList(string country);
}
