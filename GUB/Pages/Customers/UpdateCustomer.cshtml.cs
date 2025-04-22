using ViewModels.ViewModel.Customers;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;
using Services.BusinessLogic.Customers;
using Services.BusinessLogic.Validations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ViewModels.Pages.Customers
{
    [BindProperties]
    [Authorize(Roles = "Cashier")]

    public class UpdateCustomerModel : PageModel
    {

        private readonly ICustomerService _customerService;
        private readonly ICountryValidation _countryValidation;
        public UpdateCustomerModel(ICustomerService customerService,
                                   ICountryValidation countryValidation)
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
        [Required(ErrorMessage = "Birth date is required")]
        [DataType(DataType.Date)]
        public DateOnly? BirthDate { get; set; } = null;
        [Range(1, 99, ErrorMessage = "Choose a valid gender!")]
        public Gender Gender { get; set; }
        public List<SelectListItem> Genders { get; set; }
        public PhoneCode? PhoneCode { get; set; } = null;
        public List<SelectListItem> PhoneCodes { get; set; }


        public void OnGet(int id)
        {
            var c = _customerService.GetCustomer(id);

            if (Enum.TryParse<CustomerCountry>(c.CustomerCountry, out var country))
            {
                Country = country;
            }
            if (Enum.TryParse<CountryCode>(c.CustomerCountryCode, out var countryCode))
            {
                CountryCode = countryCode;
            }
            if (Enum.TryParse<PhoneCode>(c.CustomerPhoneCode, out var phoneCode))
            {
                PhoneCode = phoneCode;
            }            
            
            CustomerId = c.CustomerId;
            FirstName = c.CustomerFirstName;
            LastName = c.CustomerLastName;
            Email = c.CustomerEmail;
            PhoneNumber = c.CustomerPhone;
            Address = c.CustomerAddress;
            PostalCode = c.CustomerPostalCode;
            City = c.CustomerCity;
            SSN = c.SocialSecurityNumber;
            BirthDate = c.CustomerBirthDate;
            Gender = c.CustomerGender;


            Genders = _customerService.FillGenderList();
        }


        public IActionResult OnPost(int id)
        {
            var resp = _countryValidation.ValidateCountryCodeAndName(CountryCode, Country);

            if (resp == RespCode.InvalidCountry)
            {
                ModelState.AddModelError(
                    "Country", "Choose a valid country!");
            }

            if (ModelState.IsValid)
            {
                var UpdatedCustomer = _customerService.GetCustomer(id);

                UpdatedCustomer.CustomerId = id;
                UpdatedCustomer.CustomerFirstName = FirstName;
                UpdatedCustomer.CustomerLastName = LastName;
                UpdatedCustomer.CustomerEmail = Email;
                UpdatedCustomer.CustomerPhoneCode = ((int)PhoneCode.Value).ToString();
                UpdatedCustomer.CustomerCountryCode = CountryCode.ToString();
                UpdatedCustomer.CustomerCity = City;
                UpdatedCustomer.CustomerAddress = Address;
                UpdatedCustomer.CustomerBirthDate = BirthDate;
                UpdatedCustomer.CustomerCountry = Country.ToString();
                UpdatedCustomer.CustomerGender = Gender;
                UpdatedCustomer.CustomerPhone = PhoneNumber;
                UpdatedCustomer.CustomerPostalCode = PostalCode;
                UpdatedCustomer.SocialSecurityNumber = SSN;

                _customerService.UpdateCustomer(UpdatedCustomer);

                return RedirectToPage("CustomerDetails", new { id = id });
            }

            return Page();
        }


    }
}
