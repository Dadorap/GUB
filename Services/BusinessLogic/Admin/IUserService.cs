using DataAccessLayer.DTOs;
using ViewModels.Infrastructure.Paging;

namespace Services.BusinessLogic.Admin
{
    public interface IUserService
    {
        PagedResult<UserDTO> GetUsers(string sortColumn, string sortOrder, int page, string q);        
        void GetUser(int id);
    }
}
