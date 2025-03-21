using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

public enum RespCode
{
    OK,
    BalanceTooLow,
    IncorrectAmount,
    InvalidMessage,
    InvalidDate
}

namespace Services.BusinessLogic.Transactions
{
    public class AccountService : IAccountService
    {
        private readonly BankAppDataContext _bankAppDataContext;

        public AccountService(BankAppDataContext bankAppDataContext)
        {
            _bankAppDataContext = bankAppDataContext;
        }

        public List<AccountBalanceDTO> GetAccounts()
        {
            return _bankAppDataContext.Accounts.Select(s => new AccountBalanceDTO
            {
                AccountId = s.AccountId,
                Balance = s.Balance,
            }).ToList();
        }

        public RespCode Withdraw(int id, decimal amount, DateTime withdrawDate)
        {
            var acc = _bankAppDataContext.Accounts.First(a => a.AccountId == id);
            if (withdrawDate < DateTime.Now)
            {
                return RespCode.InvalidDate;
            }
            if (acc.Balance < amount)
            {
                return RespCode.BalanceTooLow;
            }
            if (amount < 100 && amount > 10000)
            {
                return RespCode.IncorrectAmount;
            }

            acc.Balance -= amount;
            _bankAppDataContext.Update(acc);
            _bankAppDataContext.SaveChanges();
            return RespCode.OK;
        }

        public RespCode Deposit(int id, decimal amount, DateTime date)
        {
            var acc = _bankAppDataContext.Accounts.First(a =>a.AccountId == id);

            if (date < DateTime.Now)
            {
                return RespCode.InvalidDate;
            }
            if (amount < 100 && amount > 10000)
            {
                return RespCode.IncorrectAmount;
            }

            acc.Balance += amount;
            _bankAppDataContext.Update(acc);
            _bankAppDataContext.SaveChanges();
            return RespCode.OK;
        }

        public AccountBalanceDTO GetAccount(int accountId)
        {
            var acc = _bankAppDataContext.Customers
                            .Include(c => c.Dispositions)
                            .ThenInclude(d => d.Account)
                            .FirstOrDefault(c => c.Dispositions.Any(d => d.AccountId == accountId));

            var customer = acc.Dispositions.First(d => d.AccountId == accountId);

            

            var date = DateTime.Now.AddHours(1);
            var accDto = new AccountBalanceDTO { 
                AccountId = customer.AccountId, 
                Balance = customer.Account.Balance,
                TransactionDate = date, 
                CustomerId = acc.CustomerId };
            return accDto;
        }

       
    }
}
