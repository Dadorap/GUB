using GUB.ViewModel.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.Customers;

namespace GUB.Pages.Customers
{
    public class CustomerDetailsModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerDetailsModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string SSN { get; set; } 
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public decimal TotalBalance { get; set; }
        public List<AccountBalanceViewModel> Account { get; set; }
        





        public void OnGet(int id)
        {
            var c = _customerService.GetCustomer(id);

            FullName = c.CustomerFirstName + " " + c.CustomerLastName;
            Email = c.CustomerEmail;
            PhoneNumber = c.CustomerPhone;
            Address = c.CustomerAddress + ", " + c.CustomerPostalCode;
            SSN = string.IsNullOrWhiteSpace(c.SocialSecurityNumber) ? "Missing SSN" : c.SocialSecurityNumber;
            Country = c.CustomerCountry;
            City = c.CustomerCity;
            BirthDate = c.CustomerBirthDate;
            Gender = c.CustomerGender;
            TotalBalance = c.TotalBalance;
            Account = c.Accounts.Select(a => new AccountBalanceViewModel
            {
                AccountId = a.AccountId,
                AccountNumber = a.AccountNumber,
                Balance = a.Balance,
            }).ToList();

        }
    }
}
