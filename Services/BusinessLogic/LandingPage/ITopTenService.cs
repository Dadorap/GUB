using DataAccessLayer.DTOs;

namespace Services.BusinessLogic.LandingPage
{
    public interface ITopTenService
    {
        List<TopTenDTO> GetTopTen(string countryName);
    }
}