using DataAccessLayer.DTOs;
using DataAccessLayer.Models;

namespace Services.BusinessLogic.Transactions;

public interface IAccountService
{
    List<AccountBalanceDTO> GetAccounts();
    List<AccountsDTO> GetAllAccounts();
    AccountBalanceDTO GetAccount(int accountId);
    RespCode Transaction(int id, decimal amount, DateTime withdrawDate, string transaction);
}
