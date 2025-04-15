using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Services.BusinessLogic.AccountManagement;
using Services.BusinessLogic.Customers;

namespace GUB.Pages.Customers
{
    public class RemoveCustomerModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;

        public RemoveCustomerModel(IAccountService accountService, 
            ICustomerService customerService)
        {
            _accountService = accountService;
            _customerService = customerService;
        }

        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string RemoveValidation { get; set; }
        public decimal Balance { get; set; }
        public void OnGet(int id, decimal balance)
        {
            CustomerId = id;
            Balance = balance;
            var cust = _customerService.GetCustomer(id);
            FullName = cust.CustomerFirstName + " " + cust.CustomerLastName;
        }

        public async Task<IActionResult> OnPost(int id, decimal balance)
        {
            CustomerId = id;
            Balance = balance;
            var cust = _customerService.GetCustomer(id);
            FullName = cust.CustomerFirstName + " " + cust.CustomerLastName;

            if (balance > 0)
            {
                ModelState.AddModelError("RemoveValidation", "You cannot remove a customer with a positive balance.");
            }


            if (ModelState.IsValid)
            {
                _customerService.RemoveCustomer(id);               
                return RedirectToPage("/Customers/Customer");
            }

            return Page();
        }
    }
}
