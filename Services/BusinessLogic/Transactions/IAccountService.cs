using DataAccessLayer.DTOs;
using DataAccessLayer.Models;

namespace Services.BusinessLogic.Transactions;

public interface IAccountService
{
    List<AccountBalanceDTO> GetAccounts();
    void Update(AccountBalanceDTO account);
    AccountBalanceDTO GetAccount(int accountId);
}
