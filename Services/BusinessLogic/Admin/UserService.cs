using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Infrastructure.Paging;

namespace Services.BusinessLogic.Admin
{
    public class UserService : IUserService
    {
        private readonly BankAppDataContext _bankAppDataContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(BankAppDataContext bankAppDataContext, UserManager<IdentityUser> userManager)
        {
            _bankAppDataContext = bankAppDataContext;
            _userManager = userManager;
        }


        public void GetUser(int id)
        {
            throw new NotImplementedException();
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

    }
}
