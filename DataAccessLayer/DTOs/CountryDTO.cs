namespace DataAccessLayer.DTOs;

public class CountryDTO
{
    public string Country { get; set; }
    public int TotalCustomers { get; set; }
    public int TotalAccounts { get; set; }
    public decimal TotalBalance { get; set; }
    public int TotalTransactions { get; set; }
}
