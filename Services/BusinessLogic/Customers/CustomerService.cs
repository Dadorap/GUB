using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum CountryCode
{
    SE,
    FI,
    DK,
    NO
}

enum Gender
{
    female,
    male,
    other
}
enum PhoneCode
{
    Denmark = 45,
    Sweden = 46,
    Norway = 47,
    Finland = 358
}




namespace Services.BusinessLogic.Customers
{

    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _bankAppDataContext;

        public CustomerService(BankAppDataContext bankAppDataContext)
        {
            _bankAppDataContext = bankAppDataContext;
        }

        public CustomerDetailsDTO GetCustomer(int customerId)
        {
            var q = _bankAppDataContext.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .First(q => q.CustomerId == customerId);



            var customer = new CustomerDetailsDTO
            {
                CustomerId = q.CustomerId,
                CustomerFirstName = q.Givenname,
                CustomerLastName = q.Surname,
                SocialSecurityNumber = q.NationalId,
                CustomerGender = q.Gender,
                CustomerBirthDate = q.Birthday.Value,
                CustomerEmail = q.Emailaddress,
                CustomerPhone = q.Telephonenumber,
                CustomerCity = q.City,
                CustomerCountryCode = q.CountryCode,
                CustomerPhoneCode = q.Telephonecountrycode,
                CustomerAddress = q.Streetaddress,
                CustomerCountry = q.Country,
                CustomerPostalCode = q.Zipcode,
                TotalBalance = q.Dispositions != null
                        ? q.Dispositions.Sum(d => d.Account != null ? d.Account.Balance : 0)
                        : 0,
                Accounts = q.Dispositions?
                           .Where(d => d.Account != null)
                           .Select(d => new AccountBalanceDTO
                           {
                               AccountId = d.Account.AccountId,
                               Balance = d.Account.Balance
                           })
            .ToList() ?? new()
            };

            return customer;
        }

        public List<CustomerDTO> GetCustomers(string sortColumn, string sortOrder, int pageNo, string q)
        {
            var pageSize = 50;
            var query = _bankAppDataContext.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(n => n.Givenname.Contains(q) || n.Surname.Contains(q) || n.City.Contains(q));
            }


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

            var firstItemIndex = (pageNo - 1) * pageSize;
            query = query.Skip(firstItemIndex);
            query = query.Take(pageSize);

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

        public void UpdateCustomer(CustomerDetailsDTO custDto)
        {
            var c = _bankAppDataContext.Customers.First(c => c.CustomerId == custDto.CustomerId);
            
            c.Givenname = custDto.CustomerFirstName;
            c.Surname = custDto.CustomerLastName;
            c.Gender = custDto.CustomerGender;
            c.Streetaddress = custDto.CustomerAddress;
            c.City = custDto.CustomerCity;
            c.Country = custDto.CustomerCountry;
            c.CountryCode = custDto.CustomerCountryCode;
            c.Zipcode = custDto.CustomerPostalCode;
            c.Birthday = custDto.CustomerBirthDate;
            c.NationalId = custDto.SocialSecurityNumber;
            c.Telephonenumber = custDto.CustomerPhone;
            c.Emailaddress = custDto.CustomerEmail;

            _bankAppDataContext.Update(c);
            _bankAppDataContext.SaveChanges();
        }
    }
}
