using System.Transactions;

namespace DataAccessLayer.DTOs;

public class AccountsDTO
{
    public int AccountId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public Frequency Frequency { get; set; }
}
