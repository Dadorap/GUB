using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using ViewModels.Infrastructure.Paging;

namespace Services.BusinessLogic.Admin
{
    public interface IUserService
    {
        PagedResult<UserDTO> GetUsers(string sortColumn, string sortOrder, int page, string q);
        List<SelectListItem> FillRoles();
        Task<UserDTO> GetUser(string id);
    }
}
