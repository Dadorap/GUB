using Contracts.DTOs;

namespace GUB.ViewModel.LandingPage;

public interface ICountryCardData
{
    List<LnadingPargeCardDTO> GetCountryData(string country);
}
