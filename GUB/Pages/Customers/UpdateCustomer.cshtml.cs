using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.Customers;

namespace GUB.Pages.Customers
{
    [BindProperties]
    public class UpdateCustomerModel : PageModel
    {

        private readonly ICustomerService _customerService;
        public UpdateCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string SSN { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
  
        public void OnGet(int id)
        {
            var c = _customerService.GetCustomer(id);

            FirstName = c.CustomerFirstName;
            LastName = c.CustomerLastName;
            Email = c.CustomerEmail;
            PhoneNumber = c.CustomerPhone;
            Address = c.CustomerAddress;
            PostalCode = c.CustomerPostalCode;
            Country = c.CustomerCountry;
            City = c.CustomerCity;
            SSN = c.SocialSecurityNumber;
            BirthDate = c.CustomerBirthDate;
            Gender = c.CustomerGender;
        }

    }
}
