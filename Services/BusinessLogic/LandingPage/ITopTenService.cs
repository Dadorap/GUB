using DataAccessLayer.DTOs;

namespace Services.BusinessLogic.LandingPage
{
    public interface ITopTenService
    {
        TopTenDTO GetTopTen(string countryName);
    }
}