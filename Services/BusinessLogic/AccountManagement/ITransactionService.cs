using DataAccessLayer.DTOs;

namespace Services.BusinessLogic.AccountManagement;

public interface ITransactionService
{
    List<TransactionDTO> GetTransactionDetails(int id);
    Task<RespCode> Transaction(int id, decimal amount, DateTime date, string transaction);
    RespCode Transfer(int accountNumber, int id, decimal amount, DateTime date);
}
