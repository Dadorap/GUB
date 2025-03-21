using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Services.BusinessLogic.Transactions;
using System.ComponentModel.DataAnnotations;

namespace GUB.Pages.Customers
{
    [BindProperties]
    public class DepositModel : PageModel
    {
        private readonly IAccountService _accountService;

        public DepositModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime DepositDate { get; set; }

        [Required]
        [Range(100, 10000)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "You forgot to write a comment!")]
        [MinLength(5, ErrorMessage = "Comments must be at least 5 characters long")]
        [MaxLength(100)]
        public string Comment { get; set; }



        public void OnGet(int id)
        {
            var acc = _accountService.GetAccount(id);
            AccountNumber = acc.AccountId;
            Balance = acc.Balance;
            DepositDate = acc.DepositDate;

        }

        public IActionResult OnPost(int id)
        {
            var acc = _accountService.GetAccount(id);
            AccountNumber = acc.AccountId;
            Balance = acc.Balance;

            if (DepositDate < DateTime.Now)
            {
                ModelState.AddModelError(
                "DepositDate", "Cannot Deposit money in the past!");
            }

            if (ModelState.IsValid)
            {
                var accountDb = _accountService.GetAccount(id);
                accountDb.Balance += Amount;
                _accountService.Update(accountDb);
                return RedirectToPage("Customer");
            }
            return Page();
        }
    }
}
