using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.Admin;

namespace GUB.Pages.Users
{
    public class UserTableModel : PageModel
    {
        private readonly IUserService _userService;

        public UserTableModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
            _userService.GetUsers();
        }
    }
}
