using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.Customers;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

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

        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [StringLength(100)]
        [Required]
        public string Address { get; set; }
        public string PostalCode { get; set; }
        [StringLength(50)]
        [Required]
        public string Country { get; set; }
        public string CountryCode { get; set; }
        [StringLength(50)]
        [Required]
        public string City { get; set; }
        [StringLength(50)]
        public string SSN { get; set; }
        [StringLength(50)]
        public int TelephoneCountryCode { get; set; }
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }
        [StringLength(50)]
        [Required]
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
