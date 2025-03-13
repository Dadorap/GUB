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
    public List<CountryDTO> GetCountryList()
    {
        var qurey = _bankAppDataContext.Customers
    .Include(c => c.Dispositions) 
        .ThenInclude(d => d.Account)
        .ThenInclude(t => t.Transactions)
    .AsQueryable();

        return qurey.Select(c => new CountryDTO
        {
            Country = c.Country,
            


        }).ToList();

    }
}
