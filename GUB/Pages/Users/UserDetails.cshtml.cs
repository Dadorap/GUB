using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.BusinessLogic.Admin;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using ViewModels.API;

namespace GUB.Pages.Users
{
    [BindProperties]
    [Authorize(Roles = "Admin")]

    public class UserDetailsModel : PageModel
    {
        private readonly IUserService _userService;
        public UserDetailsModel(IUserService userService)
        {
            _userService = userService;
        }
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string LoginName { get; set; }
        [Required]
        [Range(1, 99, ErrorMessage = "Choose a valid Role!")]
        public Role Role { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public string UserId { get; set; }


        public async Task OnGet(string id)
        {

            Roles = _userService.FillRoles();
            var user = await _userService.GetUser(id);
            UserId = id;
            LoginName = user.LoginName;
        }
        public async Task<IActionResult> OnPostUpdateAsync(string id)
        {
            Roles = _userService.FillRoles();


            if (!_userService.IsEmailRegex(LoginName))
            {
                ModelState.AddModelError("LoginName", "Invalid input, ex. ex@gmail.com");
            }

            if (ModelState.IsValid)
            {
                var user = await _userService.GetUser(id);
                user.LoginName = LoginName;
                user.Role = Role.ToString();


                await _userService.UpdateUser(user);

                return RedirectToPage("Index");
            }

            return Page();
        }


        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var user = await _userService.GetUser(id);
            await _userService.RemoveUser(user);

            return RedirectToPage("Index");
        }



    }
}
