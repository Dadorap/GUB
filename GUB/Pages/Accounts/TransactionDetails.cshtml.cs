using Azure;
using ViewModels.API;
using ViewModels.Infrastructure.Paging;
using ViewModels.ViewModel.Accounts;
using ViewModels.ViewModel.ZenQuotes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.AccountManagement;
using AutoMapper;

namespace ViewModels.Pages.Accounts
{
    public class TransactionDetailsModel : PageModel
    {
        private readonly ZenQuotesService _zenQuotesService;
        private readonly ITransactionDetailService _transacitonDetailService;
        private readonly IMapper _mapper;

        public TransactionDetailsModel(ZenQuotesService zenQuotesService,
            ITransactionDetailService transacitonDetailService,
            IMapper mapper)
        {
            _zenQuotesService = zenQuotesService;
            _transacitonDetailService = transacitonDetailService;
            _mapper = mapper;
        }
        public List<ZenQuotesViewModel> ZenQuotes { get; set; }
        public int AccountId { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }

        public async Task OnGet(int id, int pageNo)
        {
            ZenQuotes = (await _zenQuotesService.GetQuotes())
                        .Select(q => new ZenQuotesViewModel
                        {
                            Quote = q.Quote,
                            Author = q.Author
                        }).ToList();
            AccountId = id;


            int pageSize = 10;
            var allTrans = _transacitonDetailService.GetTransactionDetails(id);


        }

        public IActionResult OnGetShowMore(int id, int pageNo)
        {
            int pageSize = 10;

            var allTrans = _transacitonDetailService.GetTransactionDetails(id);
            int totalPages = allTrans.Count();
            PageCount = (int)Math.Ceiling((double)totalPages / pageSize);

            var pagedResult = allTrans
                             .Where(s => s.AccountId == id)
                             .AsQueryable()
                             .GetPaged(pageNo, pageSize).Results;



            var listOfTransa = _mapper.Map<List<TransactionDetailsViewModel>>(pagedResult);



            if (pageNo == 0)
                pageNo = 1;
            CurrentPage = pageNo;
            return new JsonResult(new
            {
                transa = listOfTransa,
                currentPage = CurrentPage,
                pageCount = PageCount,
            });
        }
    }
}
