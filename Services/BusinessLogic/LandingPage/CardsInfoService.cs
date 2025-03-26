using Contracts.DTOs;
using Contracts.Models;
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
    .Include(c => c.Dispositions)
        .ThenInclude(d => d.Account)
        .ThenInclude(t => t.Transactions)
        .Where(c => c.Country == country)
        .AsQueryable();

        return query.GroupBy(c => c.Country).Select(c => new CountryDTO
        {
            Country = c.Key,

            Customers = c.Select(c => c.CustomerId)
                .Distinct()
                .Count(),

            Balance = c.Sum(s => s.Dispositions.Sum(d => d.Account.Balance)),

            Transactions = c.SelectMany(s => s.Dispositions)
                   .SelectMany(d => d.Account.Transactions)
                   .Count()
        }).ToList();


    }
}
