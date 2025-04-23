using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.About;

namespace ViewModels.Pages
{
    public class AboutModel : PageModel
    {
        private readonly IAboutService _aboutService;

        public AboutModel(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public int Customers { get; set; }
        public int Accounts { get; set; }
        public int Transactions { get; set; }
        public decimal Balance { get; set; }

        public void OnGet()
        {
            var data = _aboutService.GetData();
            Customers = data.Customers;
            Accounts = data.Accounts;
            Transactions = data.Transactions;
            Balance = data.Balance;
        }
    }
}
