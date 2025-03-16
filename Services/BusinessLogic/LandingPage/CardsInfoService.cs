using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

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
        var qurey = _bankAppDataContext.Customers
    .Include(c => c.Dispositions) 
        .ThenInclude(d => d.Account)
        .ThenInclude(t => t.Transactions)
    .Where(c => c.Country == country)
    .AsQueryable();

        return qurey.GroupBy(c => c.Country).Select(c => new CountryDTO
        {
            Country = c.Key,
            Customers = c.Where(c => c.Country == country)
            .Select(c => c.CustomerId) 
            .Distinct() 
            .Count(),
            Balance = c.Sum(s => s.Dispositions.Sum(d => d.Account.Balance)),
            Transactions = c.Sum(s => s.Dispositions.Sum(d => d.Account.Transactions.Select(t => t.TransactionId).Distinct().Count()))
        }).ToList();

    }
}
