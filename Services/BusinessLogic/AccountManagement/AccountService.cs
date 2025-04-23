using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using ViewModels.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccessLayer.Data;


namespace Services.BusinessLogic.AccountManagement
{
    public class AccountService : IAccountService
    {
        private readonly BankAppDataContext _bankAppDataContext;

        public AccountService(BankAppDataContext bankAppDataContext)
        {
            _bankAppDataContext = bankAppDataContext;
        }

        public List<AccountBalanceDTO> GetAccounts()
        {
            return _bankAppDataContext.Accounts.Select(s => new AccountBalanceDTO
            {
                AccountId = s.AccountId,
                Balance = s.Balance,
            }).ToList();
        }

        public AccountBalanceDTO? GetAccount(int accountId)
        {
            var disposition = _bankAppDataContext.Dispositions
                .Include(d => d.Account)
                .FirstOrDefault(d => d.AccountId == accountId);

            if (disposition == null)
                return null;

            var accountDto = new AccountBalanceDTO
            {
                AccountId = disposition.AccountId,
                Balance = disposition.Account.Balance,
                TransactionDate = DateTime.Now, 
                CustomerId = disposition.CustomerId
            };

            return accountDto;
        }



        public PagedResult<AccountsDTO> GetAccounts(string sortColumn, string sortOrder, int page, string q)
        {

            var pageSize = 50;
            var query = _bankAppDataContext.Customers                
                .Include(c => c.Dispositions)
                    .ThenInclude(a => a.Account)
                .Where(c => c.IsActive)
                .SelectMany(d => d.Dispositions
                .Where(a => a.Account.IsActive)
                .Select(c => new AccountsDTO
                {
                    AccountId = c.Account.AccountId,
                    CustomerId = c.CustomerId,
                    CustomerFirstName = c.Customer.Givenname,
                    CustomerLastName = c.Customer.Surname,
                    Frequency = c.Account.Frequency,
                }));

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(n => n.AccountId.ToString().Contains(q) || n.CustomerFirstName.Contains(q) || n.CustomerLastName.Contains(q));
            }


            if (sortColumn == "First Name")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.CustomerFirstName);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.CustomerFirstName);

            if (sortColumn == "Last Name")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.CustomerLastName);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.CustomerLastName);

            if (sortColumn == "Accout ID")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.AccountId);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.AccountId);

            if (sortColumn == "Customer ID")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.CustomerId);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.CustomerId);
            if (sortColumn == "Frequency")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Frequency);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Frequency);





            return query.GetPaged(page, pageSize);

        }

        public AccountDTO GetAccountDTO()
        {
            var acc = new AccountDTO();
            return acc;
        }

        public List<SelectListItem> FillFrequency()
        {
            return Enum.GetValues(typeof(Frequency))
                    .Cast<Frequency>()
                    .Select(pc => new SelectListItem
                    {
                        Text = pc.ToString(),
                        Value = ((int)pc).ToString()
                    })
                    .ToList();
        }

        public List<SelectListItem> FillType()
        {
            return Enum.GetValues(typeof(TypeEnum))
                    .Cast<TypeEnum>()
                    .Select(pc => new SelectListItem
                    {
                        Text = pc.ToString(),
                        Value = ((int)pc).ToString()
                    })
                    .ToList();
        }

        public async Task CreateAccount(AccountDTO acc)
        {
            var newAcc = new Account()
            {
                Frequency = acc.Frequency,
                Created = acc.Created,
                Balance = acc.Balance,
            };

            _bankAppDataContext.Accounts.Add(newAcc);
            await _bankAppDataContext.SaveChangesAsync();

            var newDisp = new Disposition()
            {
                AccountId = newAcc.AccountId,
                CustomerId = acc.CustomerId,
                Type = acc.Type,
            };

            _bankAppDataContext.Dispositions.Add(newDisp);
            await _bankAppDataContext.SaveChangesAsync();
        }
        public void RemoveAccount(int id)
        {
            var acc = _bankAppDataContext.Accounts.FirstOrDefault(a => a.AccountId == id);
            acc.IsActive = false;
            _bankAppDataContext.SaveChanges();
        }

        public bool HasMultipleAccounts(int id)
        {
            return _bankAppDataContext.Dispositions
                .Count(d => d.CustomerId == id) > 1;
        }

        public bool IsOwner(int customerId, int accountId)
        {
            var disposition = _bankAppDataContext.Dispositions
                .FirstOrDefault(d => d.CustomerId == customerId && d.AccountId == accountId);

            return disposition?.Type == "OWNER";
        }

    }
}
