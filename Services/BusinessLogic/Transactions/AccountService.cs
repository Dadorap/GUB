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
                AccountNumber = s.AccountId,
                Balance = s.Balance,
            }).ToList();
        }

        public AccountBalanceDTO GetAccount(int accountId)
        {
            var acc = _bankAppDataContext.Accounts.First(a => a.AccountId == accountId);
            var accDto =  new AccountBalanceDTO { AccountNumber = acc.AccountId, Balance = acc.Balance};
            return accDto;
        }

        public void Update(AccountBalanceDTO account)
        {
            _bankAppDataContext.SaveChanges();
        }
    }
}
