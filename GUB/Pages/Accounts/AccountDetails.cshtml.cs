using GUB.API;
using GUB.ViewModel.Accounts;
using GUB.ViewModel.ZenQuotes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.AccountManagement;

namespace GUB.Pages.Accounts
{
    public class AccountDetailsModel : PageModel
    {
        private readonly ZenQuotesService _zenQuotesService;
        private readonly IAccountService _accountService;

        public AccountDetailsModel(ZenQuotesService zenQuotesService, IAccountService accountService)
        {
            _zenQuotesService = zenQuotesService;
            _accountService = accountService;
        }
        public List<AccountsViewModel> Accounts { get; set; }
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

            var result = _accountService.GetAccounts(SortColumn, SortOrder, CurrentPage, q);
            PageCount = result.PageCount;



            ZenQuotes = (await _zenQuotesService.GetQuotes())
                        .Select(q => new ZenQuotesViewModel
                        {
                            Quote = q.Quote,
                            Author = q.Author
                        }).ToList();
            Accounts = result.Results.Select(x => new AccountsViewModel
            {
                AccountId = x.AccountId,
                CustomerId = x.CustomerId,
                CustomerFirstName = x.CustomerFirstName,
                CustomerLastName = x.CustomerLastName,
                Frequency = x.Frequency,
            }).ToList();

        }
    }
}
