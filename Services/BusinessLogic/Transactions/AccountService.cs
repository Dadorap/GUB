using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using GUB.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;


namespace Services.BusinessLogic.Transactions
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

        public RespCode Transaction(int id, decimal amount, DateTime withdrawDate, string transaction)
        {
            var acc = _bankAppDataContext.Accounts.First(a => a.AccountId == id);
            if (withdrawDate < DateTime.Now)
            {
                return RespCode.InvalidDate;
            }
            if (acc.Balance < amount)
            {
                return RespCode.BalanceTooLow;
            }
            if (amount < 100 && amount > 10000)
            {
                return RespCode.IncorrectAmount;
            }

            if (transaction.ToLower() == "withdraw")
            {
                acc.Balance -= amount;
            }
            else if (transaction.ToLower() == "deposit")
            {
                acc.Balance += amount;

            }

            _bankAppDataContext.Update(acc);
            _bankAppDataContext.SaveChanges();
            return RespCode.OK;
        }


        public AccountBalanceDTO GetAccount(int accountId)
        {
            var acc = _bankAppDataContext.Customers
                            .Include(c => c.Dispositions)
                            .ThenInclude(d => d.Account)
                            .FirstOrDefault(c => c.Dispositions.Any(d => d.AccountId == accountId));

            var customer = acc.Dispositions.First(d => d.AccountId == accountId);



            var date = DateTime.Now.AddHours(1);
            var accDto = new AccountBalanceDTO
            {
                AccountId = customer.AccountId,
                Balance = customer.Account.Balance,
                TransactionDate = date,
                CustomerId = acc.CustomerId
            };
            return accDto;
        }


        public PagedResult<AccountsDTO> GetAccounts(string sortColumn, string sortOrder, int page, string q)
        {

            var pageSize = 50;
            var query = _bankAppDataContext.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(a => a.Account)
                .SelectMany(d => d.Dispositions.Select(c => new AccountsDTO
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


    }
}
