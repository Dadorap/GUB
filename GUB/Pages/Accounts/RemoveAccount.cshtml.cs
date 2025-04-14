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
        public decimal Balance { get; set; }

        public void OnGet(int id, decimal balance)
        {
            CustomerId = id;
            Balance = balance;
        }

        public async Task<IActionResult> OnPost(int id, decimal balance)
        {
            CustomerId = id;
            Balance = balance;
            var hasAcc = _accountService.HasMultipleAccounts(id);

            if (balance > 0)
            {
                ModelState.AddModelError("Balance", "You cannot remove an account with a positive balance.");
            }
            else if (!hasAcc)
            {
                ModelState.AddModelError("Balance", "You cannot remove your only account.");
            }



            if (ModelState.IsValid)
            {
                var accDto = new AccountDTO
                {
                    Balance = Balance,
                    CustomerId = id,
                };

                return RedirectToPage("/Customers/CustomerDetails", new { id = CustomerId });
            }

            return Page();
        }
    }
}
