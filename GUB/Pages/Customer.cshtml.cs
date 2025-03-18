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
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int CurrentPage { get; set; }
        public string Q { get; set; }


        public async Task OnGet(string sortColumn, string sortOrder,int pageNo, string q)
        {
            Q = q; 
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            CurrentPage = pageNo;


            ZenQuotes = await _zenQuotesService.GetQuotes();
            Customers = _customerService.GetCustomers(SortColumn, SortOrder, pageNo,  q)
                .Select(s => new CustomerViewModel
                {
                    Id = s.Id,
                    SSN = s.SSN,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Address = s.Address,
                    Country = s.Country,
                    City = s.City,
                }).Take(15).ToList();
        }
    }
}