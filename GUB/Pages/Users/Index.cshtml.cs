using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Services.BusinessLogic.Admin;
using ViewModels.API;
using ViewModels.ViewModel.Users;
using ViewModels.ViewModel.ZenQuotes;

namespace GUB.Pages.Users
{
    [Authorize(Roles = "Admin")]
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ZenQuotesService _zenQuotesService;
        public IndexModel(IUserService userService, ZenQuotesService zenQuotesService)
        {
            _userService = userService;
            _zenQuotesService = zenQuotesService;
        }
        public List<ZenQuotesViewModel> ZenQuotes { get; set; }
        public List<UserViewModel> Users { get; set; } = new();
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

            var result = _userService.GetUsers(SortColumn, SortOrder, CurrentPage, q);
            PageCount = result.PageCount;

            Users = result.Results.Select(u => new UserViewModel
            {
                UserId = u.UserId,
                LoginName = u.LoginName,
                Role = u.Role
            }).ToList();

            ZenQuotes = (await _zenQuotesService.GetQuotes())
                .Select(q => new ZenQuotesViewModel
                {
                    Quote = q.Quote,
                    Author = q.Author
                }).ToList();
        }

    }
}
