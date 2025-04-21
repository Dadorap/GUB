using DataAccessLayer.DTOs;

namespace Services.BusinessLogic.Admin
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetUsers();
        void GetUser(int id);
    }
}
