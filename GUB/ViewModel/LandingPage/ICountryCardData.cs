namespace GUB.ViewModel.LandingPage;

public interface ICountryCardData
{
    List<LandingPageCardViewModel> GetCountryData(string country);
}
