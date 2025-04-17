using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.BusinessLogic.AccountManagement;
using System.ComponentModel.DataAnnotations;

namespace GUB.Pages.Accounts
{
    public class RemoveAccountModel : PageModel
    {
        private readonly IAccountService _accountService;

        public RemoveAccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public string RemoveValidation { get; set; }
        

        public void OnGet(int id, decimal balance, int accountId)
        {
            CustomerId = id;
            Balance = balance;
            AccountId = accountId;
        }

        public async Task<IActionResult> OnPost(int id, decimal balance, int accountId)
        {
            CustomerId = id;
            Balance = balance;
            AccountId = accountId;
            var hasAcc = _accountService.HasMultipleAccounts(id);

            if (balance > 0)
            {
                ModelState.AddModelError("Balance", "You cannot remove an account with a positive balance.");
            }
            else if (!hasAcc)
            {
                ModelState.AddModelError("RemoveValidation", "You cannot remove your only account.");
            }



            if (ModelState.IsValid)
            {

                _accountService.RemoveAccount(AccountId);
                return RedirectToPage("/Customers/CustomerDetails", new { id = CustomerId });
            }

            return Page();
        }
    }
}
