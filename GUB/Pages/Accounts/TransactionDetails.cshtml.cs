using GUB.API;
using GUB.ViewModel.Accounts;
using GUB.ViewModel.ZenQuotes;
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
        public List<TransactionDetailsViewModel> Transactions { get; set; }
        public List<ZenQuotesViewModel> ZenQuotes { get; set; }
        public string Q { get; set; }
        public int AccountId { get; set; }
        public int CurrentPage { get; set; }

        public async Task OnGet(int id, string q)
        {
            Q = q;
            ZenQuotes = (await _zenQuotesService.GetQuotes())
                        .Select(q => new ZenQuotesViewModel
                        {
                            Quote = q.Quote,
                            Author = q.Author
                        }).ToList();

            Transactions = _transacitonDetailService.GetTransactionDetails(id).Select(s => new TransactionDetailsViewModel()
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
            AccountId = Transactions.FirstOrDefault().AccountId;

        }
    }
}
