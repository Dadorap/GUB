using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.BusinessLogic.Admin;
using System.ComponentModel.DataAnnotations;
using ViewModels.API;

namespace GUB.Pages.Users
{
    public class UserDetailsModel : PageModel
    {
        private readonly IUserService _userService;
        public UserDetailsModel(IUserService userService)
        {
            _userService = userService;
        }
        [Required]
        [EmailAddress]
        public string LoginName { get; set; }
        [Required]
        public string Role { get; set; }
        public string UserId { get; set; }

        public void OnGet()
        {
        }
    }
}
