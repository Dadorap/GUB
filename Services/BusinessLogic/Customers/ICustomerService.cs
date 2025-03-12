using DataAccessLayer.Models;

namespace Services.BusinessLogic.Customers;

public interface ICustomerService
{
    List<Customer> GetCustomers(string sortColumn, string sortOrder);
}
