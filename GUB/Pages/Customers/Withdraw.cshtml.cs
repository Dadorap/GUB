using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.Transactions;
using System.ComponentModel.DataAnnotations;

namespace GUB.Pages.Customers
{
    [BindProperties]
    public class WithdrawModel : PageModel
    {

        private readonly IAccountService _accountService;

        public WithdrawModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime WithdrawDate { get; set; }

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
            WithdrawDate = acc.DepositDate;

        }

        public IActionResult OnPost(int id)
        {
            var acc = _accountService.GetAccount(id);
            AccountNumber = acc.AccountId;
            Balance = acc.Balance;

            if (WithdrawDate < DateTime.Now)
            {               
                ModelState.AddModelError(
                "WithdrawDate", "Cannot Deposit money in the past!");
            }
            if (Amount > Balance)
            {
                ModelState.AddModelError(
                    "Amount", "You cannot Withdraw money you don't own!");
            }

            if (ModelState.IsValid)
            {
                var accountDb = _accountService.GetAccount(id);
                accountDb.Balance -= Amount;
                _accountService.Update(accountDb);
                return RedirectToPage("Customer");
            }
            return Page();
        }
    }
}
