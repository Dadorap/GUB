using GUB.ViewModel.Customers;

namespace Services.BusinessLogic.Customers;

public interface ICustomerService
{
    List<CustomerViewModel> GetSuppliers(string sortColumn, string sortOrder);
}
