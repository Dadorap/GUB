using DataAccessLayer.DTOs;
using ViewModels.Infrastructure.Paging;

namespace Services.BusinessLogic.AccountManagement;

public interface IAccountService
{
    List<AccountBalanceDTO> GetAccounts();
    AccountBalanceDTO GetAccount(int accountId);
    RespCode Transaction(int id, decimal amount, DateTime withdrawDate, string transaction);
    PagedResult<AccountsDTO> GetAccounts(string sortColumn, string sortOrder, int page, string q);
}
