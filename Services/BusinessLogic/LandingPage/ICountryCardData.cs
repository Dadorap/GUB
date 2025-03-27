using DataAccessLayer.DTOs;

namespace Services.BusinessLogic.LandingPage;

public interface ICountryCardData
{
    List<LnadingPargeCardDTO> GetCountryData(string country);
}
