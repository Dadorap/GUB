using Contracts.DTOs;
using Contracts.Models;

namespace Services.BusinessLogic.Transactions;

public interface IAccountService
{
    List<AccountBalanceDTO> GetAccounts();
    AccountBalanceDTO GetAccount(int accountId);
    RespCode Transaction(int id, decimal amount, DateTime withdrawDate, string transaction);
}
