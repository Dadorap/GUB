using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BusinessLogic.Customers
{
    
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _bankAppDataContext;

        public CustomerService(BankAppDataContext bankAppDataContext)
        {
            _bankAppDataContext = bankAppDataContext;
        }

        public List<CustomerDTO> GetCustomers(string sortColumn, string sortOrder)
        {
            var query = _bankAppDataContext.Customers.AsQueryable();

            if (sortColumn == "First Name")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Givenname);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Givenname);

            if (sortColumn == "Last Name")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Surname);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Surname);

            if (sortColumn == "Country")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Country);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Country);

            if (sortColumn == "City")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.City);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.City);

            if (sortColumn == "SSN")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.NationalId);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.NationalId);

            if (sortColumn == "Address")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Streetaddress);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Streetaddress);

            return query.Select(s => new CustomerDTO
            {
                Id = s.CustomerId,
                FirstName = s.Givenname,
                LastName = s.Surname,
                Country = s.Country,
                City = s.City,
                SSN = s.NationalId,
                Address = s.Streetaddress
            }).ToList();
        }
    }
}
