using DataAccessLayer.DTOs;
using DataAccessLayer.Models;

public enum RespCode
{
    OK,
    BalanceTooLow,
    IncorrectAmount,
    InvalidMessage
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

        public AccountBalanceDTO GetAccount(int accountId)
        {
            var acc = _bankAppDataContext.Accounts.First(a => a.AccountId == accountId);
            var date = DateTime.Now.AddHours(1);
            var accDto =  new AccountBalanceDTO { AccountId = acc.AccountId, Balance = acc.Balance, DepositDate = date};
            return accDto;
        }

        public void Update(AccountBalanceDTO account)
        {
            var acc = _bankAppDataContext.Accounts.FirstOrDefault(a => a.AccountId == account.AccountId);

            if (acc != null)
            {
                acc.Balance = account.Balance;
                _bankAppDataContext.Update(acc);
                _bankAppDataContext.SaveChanges(); 
            }
        }
    }
}
