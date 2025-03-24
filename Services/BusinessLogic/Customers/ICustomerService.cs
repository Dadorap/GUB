using DataAccessLayer.Models;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Services.BusinessLogic.Customers;

public interface ICustomerService
{
    List<CustomerDTO> GetCustomers(string sortColumn, string sortOrder, int pageNo, string q);
    CustomerDetailsDTO GetCustomer(int customerId);
    void UpdateCustomer(CustomerDetailsDTO customerDetailsDTO);
    List<SelectListItem> FillGenderList();
}
