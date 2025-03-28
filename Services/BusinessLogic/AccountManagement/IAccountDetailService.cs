using DataAccessLayer.DTOs;

namespace Services.BusinessLogic.AccountManagement;

public interface IAccountDetailService
{
    List<TransactionDTO> GetAccountDetails(int id);
}
