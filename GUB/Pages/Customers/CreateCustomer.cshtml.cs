using GUB.ViewModel.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.BusinessLogic.Customers;
using System.ComponentModel.DataAnnotations;
using Con

namespace GUB.Pages.Customers
{
    [BindProperties]
    public class CreateCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        public CreateCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
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
        public string CountryCode { get; set; }

        [StringLength(50)]
        [Required]
        public string City { get; set; }
        [StringLength(50)]
        public string? SSN { get; set; } = null;

        [StringLength(50)]
        public PhoneCode? TelephoneCountryCode { get; set; } = null;
        [DataType(DataType.Date)]
        public DateOnly? BirthDate { get; set; } = null;
        [Required]
        [Range(1, 99, ErrorMessage = "Choose a valid gender!")]
        public Enums Gender { get; set; }
        public List<SelectListItem> Genders { get; set; }
        public void OnGet(int id)
        {
            CustomerId = id;
        }

        public IActionResult OnPost()
        {


            if (ModelState.IsValid)
            {
                var newCustomer = new CustomerDTO()
                {
                    CustomerFirstName = FirstName,
                    CustomerLastName = LastName,
                    CustomerEmail = Email,
                    CustomerPhoneCode = ((int)TelephoneCountryCode.Value).ToString(),
                    CustomerCountryCode = CountryCode,
                    CustomerCity = City,
                    CustomerAddress = Address,
                    CustomerBirthDate = BirthDate,
                    CustomerCountry = Country.ToString(),
                    CustomerGender = Gender,
                    CustomerPhone = PhoneNumber,
                    CustomerPostalCode = PostalCode,
                    SocialSecurityNumber = SSN,
                };


                _customerService.CreateNewCustomer(newCustomer);

                return RedirectToPage("Customer");
            }

            return Page();
        }

    }
}
