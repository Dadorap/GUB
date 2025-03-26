using GUB.ViewModel.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.BusinessLogic.Customers;
using System.ComponentModel.DataAnnotations;
using Contracts.DTOs;
using Services.BusinessLogic.Validations;

namespace GUB.Pages.Customers
{
    [BindProperties]
    public class CreateCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ICountryValidation _countryValidation;
        
        public CreateCustomerModel(ICustomerService customerService, ICountryValidation countryValidation)
        {
            _customerService = customerService;
            _countryValidation = countryValidation;        
        }

        public int CustomerId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        public string PostalCode { get; set; }
        [Required]
        [Range(1, 99, ErrorMessage = "Choose a valid Country!")]
        public CustomerCountry Country { get; set; }
        [Range(1, 99, ErrorMessage = "Choose a valid Country code!")]
        public CountryCode CountryCode { get; set; }

        [StringLength(50)]
        [Required]
        public string City { get; set; }
        [StringLength(50)]
        public string? SSN { get; set; } = null;

        [DataType(DataType.Date)]
        public string BirthDateInput { get; set; }
        public DateOnly? BirthDate { get; set; } = null;
        [Required]
        [Range(1, 99, ErrorMessage = "Choose a valid gender!")]
        public Gender Gender { get; set; }
        public List<SelectListItem> Genders { get; set; }
        public PhoneCode? PhoneCode { get; set; } = null;
        public List<SelectListItem> PhoneCodes { get; set; }

        public void OnGet()
        {
            PhoneCodes = _customerService.FillPhoneCodes();
            PhoneCodes.Insert(0, new SelectListItem { Text = "-- Select Phone Code --", Value = "" });
        }
        public IActionResult OnPost()
        {
            var resp = _countryValidation.ValidateCountryCodeAndName(CountryCode, Country);
            if (DateOnly.TryParse(BirthDateInput, out var parsedDate))
            {
                BirthDate = parsedDate;
            }

            if (resp == RespCode.InvalidCountry)
            {
                ModelState.AddModelError(
                "Country", "Invalid Country");
            }

            if (ModelState.IsValid)
            {
                var newCustomer = new CustomerDTO()
                {
                    CustomerFirstName = FirstName,
                    CustomerLastName = LastName,
                    CustomerEmail = Email,
                    CustomerPhoneCode = ((int)PhoneCode.Value).ToString(),
                    CustomerCity = City,
                    CustomerAddress = Address,
                    CustomerBirthDate = BirthDate,
                    CustomerCountry = Country.ToString(),
                    CustomerCountryCode = CountryCode.ToString(),
                    CustomerGender = Gender,
                    CustomerPhone = PhoneNumber,
                    CustomerPostalCode = PostalCode,
                    SocialSecurityNumber = SSN,
                };


                _customerService.CreateNewCustomer(newCustomer);

                return RedirectToPage("Customer");
            }
            PhoneCodes = _customerService.FillPhoneCodes();
            PhoneCodes.Insert(0, new SelectListItem { Text = "-- Select Phone Code --", Value = "" });
            return Page();
        }

    }
}
