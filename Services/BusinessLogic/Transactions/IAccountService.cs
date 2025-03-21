using DataAccessLayer.DTOs;
using DataAccessLayer.Models;

namespace Services.BusinessLogic.Transactions;

public interface IAccountService
{
    List<AccountBalanceDTO> GetAccounts();
    AccountBalanceDTO GetAccount(int accountId);
    RespCode Withdraw(int id, decimal amount, DateTime withdrawDate);
    RespCode Deposit(int id, decimal amount, DateTime date);
}
