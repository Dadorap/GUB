using DataAccessLayer.Models;
using DataAccessLayer.DTOs;

namespace Services.BusinessLogic.Customers;

public interface ICustomerService
{
    List<CustomerDTO> GetCustomers(string sortColumn, string sortOrder, int pageNo, string q);
}
