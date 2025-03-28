using DataAccessLayer.DTOs;
using DataAccessLayer.Models;

namespace Services.BusinessLogic.AccountManagement;

public class AccountDetailService : IAccountDetailService
{
    private readonly BankAppDataContext _bankAppDataContext;

    public AccountDetailService(BankAppDataContext bankAppDataContext)
    {
        _bankAppDataContext = bankAppDataContext;
    }

    public List<TransactionDTO> GetAccountDetails(int id)
    {
       var trans = _bankAppDataContext.Transactions
            .Where(t => t.AccountId == id)
            .AsQueryable();
        if (trans == null) return null;

        var accTrans = trans.Select(s=> new TransactionDTO()
        {
            TransactionId = s.TransactionId,
            AccountId = s.AccountId,
            Date = s.Date,
            Type = s.Type,
            Operation = s.Operation,
            Amount = s.Amount,
            Balance = s.Balance,
            Symbol = s.Symbol,
            Bank = s.Bank,
            Account = s.Account,
        }).ToList();

        return accTrans;
    }
}
