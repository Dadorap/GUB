using DataAccessLayer.Models;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using GUB.Infrastructure.Paging;

namespace Services.BusinessLogic.Customers;

public interface ICustomerService
{
    PagedResult<CustomerDTO> GetCustomers(string sortColumn, string sortOrder, int pageNo, string q);
    CustomerDetailsDTO GetCustomer(int customerId);
    void UpdateCustomer(CustomerDetailsDTO customerDetailsDTO);
    List<SelectListItem> FillGenderList();
    void CreateNewCustomer(CustomerDetailsDTO customerDetailsDTO);
}
