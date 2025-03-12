using GUB.ViewModel.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.Customers;

namespace GUB.Pages
{
    [Authorize(Roles = "Cashier")]

    public class CustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public List<CustomerViewModel> Customers { get; set; }

        public void OnGet(string sortColumn, string sortOrder)
        {
            Customers = _customerService.GetCustomers(sortColumn, sortOrder)
                .Select(s => new CustomerViewModel
                {
                    Id = s.Id,  
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Country = s.Country,
                    City = s.City,
                    PhoneNumber = s.PhoneNumber,
                    Address = s.Address
                }).ToList();
        }
    }
}
