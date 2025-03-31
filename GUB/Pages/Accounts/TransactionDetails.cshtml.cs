using GUB.API;
using GUB.Infrastructure.Paging;
using GUB.ViewModel.Accounts;
using GUB.ViewModel.ZenQuotes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.AccountManagement;

namespace GUB.Pages.Accounts
{
    public class TransactionDetailsModel : PageModel
    {
        private readonly ZenQuotesService _zenQuotesService;
        private readonly ITransactionDetailService _transacitonDetailService;

        public TransactionDetailsModel(ZenQuotesService zenQuotesService, ITransactionDetailService transacitonDetailService)
        {
            _zenQuotesService = zenQuotesService;
            _transacitonDetailService = transacitonDetailService;
        }
        public List<ZenQuotesViewModel> ZenQuotes { get; set; }
        public int AccountId { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }

        public  async Task OnGet(int id)
        {
            ZenQuotes = (await _zenQuotesService.GetQuotes())
                        .Select(q => new ZenQuotesViewModel
                        {
                            Quote = q.Quote,
                            Author = q.Author
                        }).ToList();
            AccountId = id;
        }

        public IActionResult OnGetShowMore(int id, int pageNo)
        {

            var listOfTransa = _transacitonDetailService.GetTransactionDetails(id)
                .Where(s => s.AccountId == id)
                .AsQueryable()
                .GetPaged(pageNo, 10).Results
                .Select(s => new TransactionDetailsViewModel()
                {
                    TransactionId = s.TransactionId,
                    AccountId = s.AccountId,
                    Date = s.Date,
                    Type = s.Type,
                    Operation = s.Operation,
                    Amount = s.Amount,
                    Balance = s.Balance,
                    Symbol = s.Symbol,
                    Bank = s.Bank,
                    Account = s.Account
                }).ToList();
            PageCount = listOfTransa.Count;

            return new JsonResult(new { transa = listOfTransa });
        }
    }
}
