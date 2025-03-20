using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.Transactions;

namespace GUB.Pages.Customers
{
    public class DepositModel : PageModel
    {
        private readonly IAccountService _accountService;

        public DepositModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public void OnGet(int id)
        {
            var acc = _accountService.GetAccount(id);
            Id = acc.AccountId;
            AccountNumber = acc.AccountNumber;
            Balance = acc.Balance;

        }
    }
}
