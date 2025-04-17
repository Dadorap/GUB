using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.BusinessLogic.AccountManagement;

public class TransactionService : ITransactionService
{
    private readonly BankAppDataContext _bankAppDataContext;

    public TransactionService(BankAppDataContext bankAppDataContext)
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

    public RespCode Transaction(int id, decimal amount, DateTime date, string transaction)
    {
        var acc = _bankAppDataContext.Accounts.First(a => a.AccountId == id);

        if (date.Date < DateTime.Now.Date)
        {
            return RespCode.InvalidDate;
        }
        if (transaction.ToLower() == "withdraw")
        {
            if (acc.Balance < amount)
            {
                return RespCode.BalanceTooLow;
            }
        }
        if (amount < 100 || amount > 10000)
        {
            return RespCode.IncorrectAmount;
        }

        if (transaction.ToLower() == "withdraw")
        {
            acc.Balance -= amount;
        }
        else if (transaction.ToLower() == "deposit")
        {
            acc.Balance += amount;

        }

        _bankAppDataContext.Update(acc);
        _bankAppDataContext.SaveChanges();
        return RespCode.OK;
    }

    public RespCode Transfer(int accountNumber, int id, decimal amount, DateTime date)
    {
        if (date.Date < DateTime.Now.Date)
            return RespCode.InvalidDate;

        if (amount < 100 || amount > 10000)
            return RespCode.IncorrectAmount;

        var fromAcc = _bankAppDataContext.Accounts.FirstOrDefault(a => a.AccountId == id);
        var toAcc = _bankAppDataContext.Accounts.FirstOrDefault(a => a.AccountId == accountNumber);

        if (fromAcc == null || toAcc == null)
            return RespCode.InvalidAccountNumber;

        if (fromAcc.Balance < amount)
            return RespCode.BalanceTooLow;

        fromAcc.Balance -= amount;
        toAcc.Balance += amount;

        _bankAppDataContext.SaveChanges();
        return RespCode.OK;
    }
}
