using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using ViewModels.Infrastructure.Paging;

namespace Services.BusinessLogic.Admin
{
    [Authorize(Roles = "Admin")]
    [BindProperties]
    public class UserService : IUserService
    {
        private readonly BankAppDataContext _bankAppDataContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(BankAppDataContext bankAppDataContext, UserManager<IdentityUser> userManager)
        {
            _bankAppDataContext = bankAppDataContext;
            _userManager = userManager;
        }


        public async Task<UserDTO?> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return null;

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            return new UserDTO
            {
                UserId = user.Id,
                LoginName = user.Email,
                Role = role
            };
        }


        public List<IdentityUser> Users { get; set; }
        public List<UserDTO> UsersWithRoles { get; set; }
        public PagedResult<UserDTO> GetUsers(string sortColumn, string sortOrder, int page, string q)
        {
            var pageSize = 50;
            var query = _bankAppDataContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(u => u.Id.Contains(q) || u.UserName.Contains(q));
            }

            if (sortColumn == "ID")
                query = sortOrder == "asc" ? query.OrderBy(u => u.Id) : query.OrderByDescending(u => u.Id);

            if (sortColumn == "Username")
                query = sortOrder == "asc" ? query.OrderBy(u => u.UserName) : query.OrderByDescending(u => u.UserName);

            var projectedQuery = query.Select(u => new UserDTO
            {
                UserId = u.Id,
                LoginName = u.Email,
                Role = ""
            });

            var paged = projectedQuery.GetPaged(page, pageSize);

            foreach (var user in paged.Results)
            {
                var identityUser = _bankAppDataContext.Users.First(u => u.Id == user.UserId);
                var roles = _userManager.GetRolesAsync(identityUser).Result;
                user.Role = string.Join(", ", roles);
            }

            return paged;
        }

        public List<SelectListItem> FillRoles()
        {
            var roleList = Enum.GetValues<Role>()
                .Select(g => new SelectListItem()
                {
                    Value = ToString(),
                    Text = ToString(),
                }).ToList();

            return roleList;

        }

        public async Task UpdateUser(UserDTO user)
        {
            var identityUser = await _userManager.FindByIdAsync(user.UserId);
            if (identityUser == null)
            {
                throw new Exception("User not found.");
            }

            identityUser.Email = user.LoginName;
            identityUser.UserName = user.LoginName;

            identityUser.NormalizedUserName = user.LoginName.ToUpper();
            identityUser.NormalizedEmail = user.LoginName.ToUpper();

            await _userManager.UpdateAsync(identityUser);

            var currentRoles = await _userManager.GetRolesAsync(identityUser);
            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(identityUser, currentRoles);
            }

            await _userManager.AddToRoleAsync(identityUser, user.Role);
        }

        public bool IsEmailRegex(string loginName)
        {
            return Regex.IsMatch(loginName, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        public async Task<bool> HasAtLeastOneOtherAdminAsync(string userIdToExclude)
        {
            var allAdmins = await _userManager.GetUsersInRoleAsync("Admin");

            return allAdmins.Any(u => u.Id != userIdToExclude);
        }

        public async Task RemoveUser(UserDTO user)
        {
            var identityUser = await _userManager.FindByIdAsync(user.UserId);
            await _userManager.DeleteAsync(identityUser);
        }

    }
}
