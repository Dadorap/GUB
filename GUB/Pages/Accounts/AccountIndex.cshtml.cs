using GUB.API;
using GUB.ViewModel.Customers;
using GUB.ViewModel.ZenQuotes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GUB.Pages.Accounts
{
    public class AccountIndexModel : PageModel
    {
        private readonly ZenQuotesService _zenQuotesService;

        public AccountIndexModel(ZenQuotesService zenQuotesService)
        {
            _zenQuotesService = zenQuotesService;
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

            //var result = _customerService.GetCustomers(SortColumn, SortOrder, CurrentPage, q);
            //PageCount = result.PageCount;



            ZenQuotes = (await _zenQuotesService.GetQuotes())
                        .Select(q => new ZenQuotesViewModel
                        {
                            Quote = q.Quote,
                            Author = q.Author
                        }).ToList();
        }
    }
}
