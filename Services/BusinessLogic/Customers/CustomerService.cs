using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using ViewModels.Infrastructure.Paging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet.DependencyResolver;
using Disposition = DataAccessLayer.Models.Disposition;
using DataAccessLayer.Data;





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
                .ThenInclude(d => d.Account).Where(acc => acc.IsActive)
                .First(q => q.CustomerId == customerId);



            var customer = new CustomerDetailsDTO
            {
                CustomerId = q.CustomerId,
                CustomerFirstName = q.Givenname,
                CustomerLastName = q.Surname,
                SocialSecurityNumber = q.NationalId,
                CustomerGender = q.GenderEnum,
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
                               Balance = d.Account.Balance,
                               IsActive = d.Account.IsActive
                           })
            .ToList() ?? new()
            };

            return customer;
        }

        public PagedResult<CustomerDTO> GetCustomers(string sortColumn, string sortOrder, int page, string q)
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



            var dtoQuery = query
                .Where(c => c.IsActive == true)
                .Select(s => new CustomerDTO
                {
                    Id = s.CustomerId,
                    CustomerFirstName = s.Givenname,
                    CustomerLastName = s.Surname,
                    CustomerCountry = s.Country,
                    CustomerCity = s.City,
                    SocialSecurityNumber = s.NationalId,
                    CustomerAddress = s.Streetaddress,
                    IsActive = s.IsActive,
                });
            return dtoQuery.GetPaged(page, pageSize);

        }

        public void UpdateCustomer(CustomerDetailsDTO custDto)
        {
            var c = _bankAppDataContext.Customers.First(c => c.CustomerId == custDto.CustomerId);

            c.Givenname = custDto.CustomerFirstName;
            c.Surname = custDto.CustomerLastName;
            c.GenderEnum = custDto.CustomerGender;
            c.Streetaddress = custDto.CustomerAddress;
            c.City = custDto.CustomerCity;
            c.Country = custDto.CustomerCountry;
            c.CountryCode = custDto.CustomerCountryCode.ToString();
            c.Zipcode = custDto.CustomerPostalCode;
            c.Birthday = custDto.CustomerBirthDate;
            c.NationalId = custDto.SocialSecurityNumber;
            c.Telephonenumber = custDto.CustomerPhone;
            c.Telephonecountrycode = custDto.CustomerPhoneCode;
            c.Emailaddress = custDto.CustomerEmail;

            _bankAppDataContext.Update(c);
            _bankAppDataContext.SaveChanges();
        }


        public List<SelectListItem> FillGenderList()
        {
            var genderList = Enum.GetValues<Gender>()
                .Select(g => new SelectListItem()
                {
                    Value = ToString(),
                    Text = ToString(),
                }).ToList();

            return genderList;
        }

        public List<SelectListItem> FillPhoneCodes()
        {
            return Enum.GetValues(typeof(PhoneCode))
                    .Cast<PhoneCode>()
                    .Select(pc => new SelectListItem
                    {
                        Text = pc.ToString(),
                        Value = ((int)pc).ToString()
                    })
                    .ToList();
        }

        public void CreateNewCustomer(CustomerDTO c)
        {
            var newCustomer = new Customer()
            {
                Givenname = c.CustomerFirstName,
                Surname = c.CustomerLastName,
                GenderEnum = c.CustomerGender,
                Streetaddress = c.CustomerAddress,
                Zipcode = c.CustomerPostalCode,
                CountryCode = c.CustomerCountryCode,
                Country = c.CustomerCountry,
                City = c.CustomerCity,
                Telephonecountrycode = c.CustomerPhoneCode,
                Telephonenumber = c.CustomerPhone,
                NationalId = c.SocialSecurityNumber,
                Birthday = c.CustomerBirthDate,
                Emailaddress = c.CustomerEmail,
                Dispositions = new List<Disposition>()

            };
            var newAccount = new Account()
            {
                Frequency = "Monthly",
                Balance = 0,
                Created = DateOnly.FromDateTime(DateTime.Now),
                Dispositions = new List<Disposition>()
            };

            var disposition = new Disposition()
            {
                Type = "OWNER",
                Customer = newCustomer,
                Account = newAccount,
            };

            newCustomer.Dispositions.Add(disposition);
            newAccount.Dispositions.Add(disposition);

            _bankAppDataContext.Customers.Add(newCustomer);
            _bankAppDataContext.SaveChanges();
        }

        public CustomerDTO GetCustomerDTO()
        {
            var customer = new CustomerDTO();
            return customer;
        }

        public void RemoveCustomer(int id)
        {
            var csutomer = _bankAppDataContext.Customers.FirstOrDefault(a => a.CustomerId == id);
            csutomer.IsActive = false;
            _bankAppDataContext.SaveChanges();
        }
    }
}
