using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        }).OrderByDescending(d => d.TransactionId)
        .ToList();



        return accTrans;
    }

    public async Task<RespCode> Transaction(int id, decimal amount, DateTime date, string transaction, string comment)
    {
        var acc = _bankAppDataContext.Accounts.First(a => a.AccountId == id);
        decimal totalBalance;

        if (date.Date < DateTime.Now.Date)
        {
            return RespCode.InvalidDate;
        }

        if (transaction == "withdraw")
        {
            if (acc.Balance < amount)
            {
                return RespCode.BalanceTooLow;
            }
        }

        if (amount < 100 || amount > 20000)
        {
            return RespCode.IncorrectAmount;
        }

        Transaction newTrans;

        if (transaction == "withdraw")
        {
            acc.Balance -= amount;
            totalBalance = acc.Balance;
            newTrans = new Transaction()
            {
                AccountId = id,
                Amount = amount,
                Balance = totalBalance,
                Date = DateOnly.FromDateTime(date),
                Type = "Debit",
                Operation = "Withdrawal in Cash",
                Symbol = comment
            };
        }
        else 
        {
            acc.Balance += amount;
            totalBalance = acc.Balance;
            newTrans = new Transaction()
            {
                AccountId = id,
                Amount = amount,
                Balance = totalBalance,
                Date = DateOnly.FromDateTime(date),
                Type = "Credit",
                Operation = "Credit in Cash",
                Symbol = comment
            };
        }


        _bankAppDataContext.Transactions.Add(newTrans);
        await _bankAppDataContext.SaveChangesAsync();

        return RespCode.OK;
    }




    public RespCode Transfer(int toAccountId, int fromAccountId, decimal amount, DateTime date, string comment)
    {
        if (date.Date < DateTime.Now.Date)
            return RespCode.InvalidDate;

        if (amount < 100 || amount > 30000)
            return RespCode.IncorrectAmount;

        var fromAcc = _bankAppDataContext.Accounts.FirstOrDefault(a => a.AccountId == fromAccountId);
        var toAcc = _bankAppDataContext.Accounts.FirstOrDefault(a => a.AccountId == toAccountId);

        if (fromAcc == null || toAcc == null)
            return RespCode.InvalidAccountNumber;

        if (fromAcc.Balance < amount)
            return RespCode.BalanceTooLow;

        fromAcc.Balance -= amount;
        var fromTransaction = new Transaction()
        {
            AccountId = fromAccountId,
            Amount =  amount,
            Balance = fromAcc.Balance,
            Date = DateOnly.FromDateTime(date),
            Type = "Debit",
            Operation = "Transfer to another account",
            Symbol = comment
        };

        toAcc.Balance += amount;
        var toTransaction = new Transaction()
        {
            AccountId = toAccountId,
            Amount = amount,
            Balance = toAcc.Balance,
            Date = DateOnly.FromDateTime(date),
            Type = "Credit",
            Operation = "Transfer from another account",
            Symbol = comment
        };

        _bankAppDataContext.Transactions.Add(fromTransaction);
        _bankAppDataContext.Transactions.Add(toTransaction);

        _bankAppDataContext.SaveChanges();
        return RespCode.OK;
    }

}
