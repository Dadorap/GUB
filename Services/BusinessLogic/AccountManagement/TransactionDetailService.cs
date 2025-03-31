using DataAccessLayer.DTOs;
using DataAccessLayer.Models;

namespace Services.BusinessLogic.AccountManagement;

public class TransactionDetailService : ITransactionDetailService
{
    private readonly BankAppDataContext _bankAppDataContext;

    public TransactionDetailService(BankAppDataContext bankAppDataContext)
    {
        _bankAppDataContext = bankAppDataContext;
    }

    public List<TransactionDTO> GetTransactionDetails(int id)
    {
        var trans = _bankAppDataContext.Transactions
             .Where(t => t.AccountId == id)
             .AsQueryable();
        

        var accTrans = trans.Select(s => new TransactionDTO()
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
        }).OrderByDescending(d => d.Date)
          .ToList();

        return accTrans;
    }
}
