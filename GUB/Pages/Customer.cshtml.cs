using DataAccessLayer.DTOs;
using GUB.API;
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
        private readonly ZenQuotesService _zenQuotesService;


        public CustomerModel(ICustomerService customerService, ZenQuotesService zenQuotesService)
        {
            _zenQuotesService = zenQuotesService;
            _customerService = customerService;
        }

        public List<CustomerViewModel> Customers { get; set; }
        public List<ZenQuotesDTO> ZenQuotes { get; set; } = new();


        public async Task OnGet(string sortColumn, string sortOrder)
        {
            ZenQuotes = await _zenQuotesService.GetQuotes();
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
                }).Take(15).ToList();
        }
    }
}