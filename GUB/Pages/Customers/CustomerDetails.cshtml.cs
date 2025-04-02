using ViewModels.ViewModel.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.Customers;

namespace ViewModels.Pages.Customers
{
    [Authorize(Roles = "Cashier")]

    public class CustomerDetailsModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerDetailsModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? TelephoneCountryCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string SSN { get; set; } 
        public DateOnly? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public decimal TotalBalance { get; set; }
        public List<AccountBalanceViewModel> Account { get; set; }
        





        public void OnGet(int id)
        {
            var c = _customerService.GetCustomer(id);
            CustomerId = c.CustomerId;
            FullName = c.CustomerFirstName + " " + c.CustomerLastName;
            Email = c.CustomerEmail;
            PhoneNumber = c.CustomerPhone;
            TelephoneCountryCode = c.CustomerPhoneCode;
            Address = c.CustomerAddress + ", " + c.CustomerPostalCode;
            SSN = string.IsNullOrWhiteSpace(c.SocialSecurityNumber) ? "Missing SSN" : c.SocialSecurityNumber;
            Country = c.CustomerCountry;
            City = c.CustomerCity;
            CountryCode = c.CustomerCountryCode;
            BirthDate = c.CustomerBirthDate;
            Gender = c.CustomerGender;
            TotalBalance = c.TotalBalance;
            Account = c.Accounts.Select(a => new AccountBalanceViewModel
            {
                AccountId = a.AccountId,
                Balance = a.Balance,
            }).ToList();

        }
    }
}
