using GUB.ViewModel.Customers;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;
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

        public int CustomerId { get; set; }
        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        [StringLength(100)]
        [Required]
        public string Address { get; set; }
        public string PostalCode { get; set; }
        [StringLength(50)]
        [Required]
        public string Country { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [StringLength(50)]
        [Required]
        public string City { get; set; }
        [StringLength(50)]
        public string? SSN { get; set; } = null;

        [StringLength(50)]
        public string? TelephoneCountryCode { get; set; } = null;
        [DataType(DataType.Date)]
        public DateOnly? BirthDate { get; set; } = null;
        [Range(1,99, ErrorMessage = "Choose a valid gender!")]
        public Gender Gender { get; set; }
        public List<SelectListItem> Genders { get; set; }


        public void OnGet(int id)
        {
            var c = _customerService.GetCustomer(id);

            CustomerId = c.CustomerId;
            FirstName = c.CustomerFirstName;
            LastName = c.CustomerLastName;
            Email = c.CustomerEmail;
            PhoneNumber = c.CustomerPhone;
            Address = c.CustomerAddress;
            PostalCode = c.CustomerPostalCode;
            Country = c.CustomerCountry;
            CountryCode = c.CustomerCountryCode;
            City = c.CustomerCity;
            SSN = c.SocialSecurityNumber;
            BirthDate = c.CustomerBirthDate;            
            TelephoneCountryCode =  c.CustomerPhoneCode;

            Genders = _customerService.FillGenderList();
        }


        public IActionResult OnPost(int id)
        {


            if (ModelState.IsValid)
            {
            var UpdatedCustomer = _customerService.GetCustomer(id);

                UpdatedCustomer.CustomerId = id;
                UpdatedCustomer.CustomerFirstName = FirstName;
                UpdatedCustomer.CustomerLastName = LastName;
                UpdatedCustomer.CustomerEmail = Email;
                UpdatedCustomer.CustomerPhoneCode = TelephoneCountryCode;
                UpdatedCustomer.CustomerCountryCode = CountryCode;
                UpdatedCustomer.CustomerCity = City;
                UpdatedCustomer.CustomerAddress = Address;
                UpdatedCustomer.CustomerBirthDate = BirthDate;
                UpdatedCustomer.CustomerCountry = Country;
                UpdatedCustomer.CustomerGender = Gender;
                UpdatedCustomer.CustomerPhone = PhoneNumber;
                UpdatedCustomer.CustomerPostalCode = PostalCode;
                UpdatedCustomer.SocialSecurityNumber =SSN;

                _customerService.UpdateCustomer(UpdatedCustomer);

                return RedirectToPage("CustomerDetails" , new {id = id});
            }

            return Page();
        }


    }
}
