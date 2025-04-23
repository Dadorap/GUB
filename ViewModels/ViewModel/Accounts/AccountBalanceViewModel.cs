namespace ViewModels.ViewModel.Accounts
{
    public class AccountBalanceViewModel
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public DateTime DepositDate { get; set; }
        public int CustomerId { get; set; }
        public bool IsActive { get; set; }

    }
}
