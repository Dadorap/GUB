namespace ViewModels.ViewModel.Accounts
{
    public class AccountViewModel
    {
        public string Frequency { get; set; } = null!;
        public DateOnly Created { get; set; }
        public decimal Balance { get; set; }
        public int CustomerId { get; set; }
        public TypeEnum Type { get; set; }
    }
}
