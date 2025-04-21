using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<List<UserDTO>> GetUsers()
        {
            Users = _bankAppDataContext.Users.ToList();
            UsersWithRoles = new List<UserDTO>();

            foreach (var user in Users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UsersWithRoles.Add(new UserDTO
                {
                    UserId = user.Id,
                    LoginName = user.Email,
                    Role = string.Join(", ", roles)
                });
            }

            return  UsersWithRoles;
        }
    }
}
