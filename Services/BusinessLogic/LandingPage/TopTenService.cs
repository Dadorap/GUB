using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.BusinessLogic.LandingPage;

public class TopTenService : ITopTenService
{
    private readonly BankAppDataContext _bankAppDataContext;

    public TopTenService(BankAppDataContext bankAppDataContext)
    {
        _bankAppDataContext = bankAppDataContext;
    }

    public List<TopTenDTO> GetTopTen(string countryName)
    {
        var topTenDTO = _bankAppDataContext.Customers
            .Include(d => d.Dispositions)
            .ThenInclude(a => a.Account)
            .Where(c => c.Country == countryName)
            .OrderByDescending(a => a.Dispositions
            .Sum(d => d.Account.Balance))
            .Take(10);

        return topTenDTO.Select(s => new TopTenDTO()
        {
            AccoutId = s.Dispositions.Select(s => s.AccountId).First(),
            Name = s.Givenname + " " + s.Surname,
            SSN = s.NationalId,
            Balance = s.Dispositions.Select(s => s.Account.Balance).First()
            
        }).ToList();



    }
}
