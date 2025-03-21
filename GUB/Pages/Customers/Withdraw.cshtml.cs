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
        public int CustomerId { get; set; }

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
            WithdrawDate = acc.TransactionDate;
            CustomerId = acc.CustomerId;

        }

        public IActionResult OnPost(int id)
        {
            var resp = _accountService.Transaction(id, Amount, WithdrawDate, "withdraw");
            var acc = _accountService.GetAccount(id);
            AccountNumber = acc.AccountId;
            Balance = acc.Balance;
            CustomerId = acc.CustomerId;


            if (resp == RespCode.InvalidDate)
            {               
                ModelState.AddModelError(
                "WithdrawDate", "Cannot Deposit money in the past!");
            }
            if (resp == RespCode.BalanceTooLow)
            {
                ModelState.AddModelError(
                    "Amount", "You cannot Withdraw money you don't own!");
            }
            if (resp == RespCode.IncorrectAmount)
            {
                ModelState.AddModelError(
                    "Amount", "Amount must be between 100 and 10,000.");
            }

            if (ModelState.IsValid)
            {
                if (resp == RespCode.OK)
                {
                    return RedirectToPage("CustomerDetails", new { id = CustomerId });
                }
            }
            
            return Page();
        }
    }
}
