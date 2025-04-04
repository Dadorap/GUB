namespace DataAccessLayer.DTOs
{
    public class AccountDTO
    {

        public string Frequency { get; set; } = null!;

        public DateOnly Created { get; set; }

        public decimal Balance { get; set; }
    }
}
