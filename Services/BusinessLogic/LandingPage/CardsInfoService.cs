using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.BusinessLogic.LandingPage;

public class CardsInfoService : ICardsInfoService
{

    private readonly BankAppDataContext _bankAppDataContext;

    public CardsInfoService(BankAppDataContext bankAppDataContext)
    {
        _bankAppDataContext = bankAppDataContext;
    }
    public List<CountryDTO> GetCountryList(string country)
    {
        var query = _bankAppDataContext.Customers
            .Where(c => c.IsActive == true && c.Country == country)
            .Include(c => c.Dispositions)
            .ThenInclude(d => d.Account)
            .AsQueryable();


        return query.GroupBy(c => c.Country).Select(c => new CountryDTO
        {
            Country = c.Key,

            Customers = c.Select(c => c.CustomerId)
                .Distinct()
                .Count(),

            Balance = c.Sum(s => s.Dispositions.Sum(d => d.Account.Balance)),

            Accounts = c.SelectMany(c => c.Dispositions)
                .Where(d => d.Account.IsActive == true)
                .Select(d => d.AccountId)
                .Distinct()
                .Count()

        }).ToList();
    }
}
