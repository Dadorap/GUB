namespace ViewModels.ViewModel.Customers
{
    public class AccountBalanceViewModel
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public DateTime DepositDate { get; set; }
        public int CustomerId { get; set; }
    }
}
