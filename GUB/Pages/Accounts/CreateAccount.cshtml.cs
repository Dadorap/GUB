using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.BusinessLogic.AccountManagement;
using System.ComponentModel.DataAnnotations;

namespace GUB.Pages.Accounts
{
    [BindProperties]
    public class CreateAccountModel : PageModel
    {
        private readonly IAccountService _accountService;

        public CreateAccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public int CustomerId { get; set; }
        [Range(1, 99, ErrorMessage = "Choose a valid Frequency!")]
        public Frequency Frequency { get; set; }
        public List<SelectListItem> Frequencies { get; set; }
        [Range(typeof(decimal), "100", "10000", ErrorMessage = "Amount must be between 100 and 10000.")]
        public decimal Balance { get; set; }
        public string Type { get; set; } = "OWNER";

        public void OnGet(int id)
        {
            CustomerId = id;
            Frequencies = _accountService.FillFrequency();
        }

        public  async Task<IActionResult> OnPost(int id)
        {
            CustomerId = id;

            if (ModelState.IsValid) 
            {
                var accDto = new AccountDTO
                {
                    Balance = Balance,
                    Type = Type,
                    Created = DateOnly.FromDateTime(DateTime.Now),
                    CustomerId = id,
                    Frequency = Frequency.ToString()
                };

                await _accountService.CreateAccount(accDto);
                return RedirectToPage("/Customers/CustomerDetails", new { id = CustomerId });
            }

            Frequencies = _accountService.FillFrequency();
            return Page();
        }
    }
}
