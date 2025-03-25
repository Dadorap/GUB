using GUB.API;
using GUB.ViewModel.Customers;
using GUB.ViewModel.ZenQuotes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.Customers;

namespace GUB.Pages.Customers
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
        public List<ZenQuotesViewModel> ZenQuotes { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int CurrentPage { get; set; }
        public string Q { get; set; }
        public int PageCount { get; set; }


        public async Task OnGet(string sortColumn, string sortOrder, int pageNo, string q)
        {
            Q = q;
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            if (pageNo == 0)
                pageNo = 1;
            CurrentPage = pageNo;

            var result = _customerService.GetCustomers(SortColumn, SortOrder, CurrentPage, q);
            PageCount = result.PageCount;



            ZenQuotes = (await _zenQuotesService.GetQuotes())
                        .Select(q => new ZenQuotesViewModel
                        {
                            Quote = q.Quote,
                            Author = q.Author
                        }).ToList();

            Customers = result.Results
                .Select (s => new CustomerViewModel
                {
                    Id = s.Id,
                    SSN = s.SSN,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Address = s.Address,
                    Country = s.Country,
                    City = s.City,
                }).ToList();
        }
    }
}