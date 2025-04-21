using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using ViewModels.Infrastructure.Paging;

namespace Services.BusinessLogic.Admin
{
    public interface IUserService
    {
        PagedResult<UserDTO> GetUsers(string sortColumn, string sortOrder, int page, string q);
        List<SelectListItem> FillRoles();
        Task<UserDTO> GetUser(string id);
        Task UpdateUser(UserDTO user);
        Task RemoveUser(UserDTO user);
        bool IsEmailRegex(string loginName);
        Task<bool> HasAtLeastOneOtherAdminAsync(string userIdToExclude);

    }
}
