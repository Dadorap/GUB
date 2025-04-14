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


            if (ModelState.IsValid)
            {
                var accDto = new AccountDTO
                {
                    Balance = Balance,
                    CustomerId = id,
                };

                await _accountService.CreateAccount(accDto);
                return RedirectToPage("/Customers/CustomerDetails", new { id = CustomerId });
            }

            return Page();
        }
    }
}
