using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.BusinessLogic.Admin;
using System.ComponentModel.DataAnnotations;
using System.Data;
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
        public List<SelectListItem> Roles { get; set; }
        public string UserId { get; set; }

        public async Task OnGet(string id)
        {

            Roles= _userService.FillRoles();
            var user = await _userService.GetUser(id);
            UserId = id;
            LoginName = user.LoginName;
            Role = user.Role;

        }
    }
}
