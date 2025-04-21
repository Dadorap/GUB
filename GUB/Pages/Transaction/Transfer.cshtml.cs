using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.AccountManagement;
using System.ComponentModel.DataAnnotations;

namespace GUB.Pages.Transaction
{
    [BindProperties]
    public class TransferModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public TransferModel(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public int AccountNumber { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Account number must be at least 1.")]
        public int ToAccountNumber { get; set; }

        public decimal Balance { get; set; }
        public DateTime TransferDate { get; set; }
        public int CustomerId { get; set; }
        [Range(typeof(decimal), "100", "10000", ErrorMessage = "Amount must be between 100 and 10000.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "You forgot to write a comment!")]
        [MinLength(5, ErrorMessage = "Comments must be at least 5 characters long")]
        [MaxLength(100)]
        public string Comment { get; set; }



        public void OnGet(int id, int customerId)
        {
            var acc = _accountService.GetAccount(id);
            AccountNumber = acc.AccountId;
            Balance = acc.Balance;
            TransferDate = acc.TransactionDate;
            CustomerId = customerId;
        }

        public IActionResult OnPost(int id, int customerId)
        {
            var resp = _transactionService.Transfer(ToAccountNumber, id, Amount, TransferDate, Comment);
           
            var acc = _accountService.GetAccount(id);
            AccountNumber = acc.AccountId;
            Balance = acc.Balance;
            CustomerId = customerId;


            if (resp == RespCode.InvalidDate)
            {
                ModelState.AddModelError(
                "DepositDate", "Cannot Deposit money in the past!");
            }
            if (resp == RespCode.InvalidAccountNumber)
            {
                ModelState.AddModelError("ToAccountNumber", "Account number does not exist!");
            }
            if (resp == RespCode.BalanceTooLow)
            {
                ModelState.AddModelError(
                    "Amount", "You cannot Send money you don't own!");
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
                    return RedirectToPage("/Customers/CustomerDetails", new { id = CustomerId });
                }
            }
            return Page();
        }
    }
}
