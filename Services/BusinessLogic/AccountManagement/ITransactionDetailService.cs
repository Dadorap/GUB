using DataAccessLayer.DTOs;

namespace Services.BusinessLogic.AccountManagement;

public interface ITransactionDetailService
{
    List<TransactionDTO> GetTransactionDetails(int id);
}
