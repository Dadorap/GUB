using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using ViewModels.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


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

        public RespCode Transaction(int id, decimal amount, DateTime date, string transaction)
        {
            var acc = _bankAppDataContext.Accounts.First(a => a.AccountId == id);

            if (date.Date < DateTime.Now.Date)
            {
                return RespCode.InvalidDate;
            }
            if (transaction.ToLower() == "withdraw")
            {
                if (acc.Balance < amount)
                {
                    return RespCode.BalanceTooLow;
                }
            }
            if (amount < 100 || amount > 10000)
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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR CREATING ACCOUNT: " + ex.Message);
                throw;
            }
        }

        public RespCode Transfer(int accountNumber, int id, decimal amount, DateTime date)
        {
            if (date.Date < DateTime.Now.Date)
                return RespCode.InvalidDate;

            if (amount < 100 || amount > 10000)
                return RespCode.IncorrectAmount;

            var fromAcc = _bankAppDataContext.Accounts.FirstOrDefault(a => a.AccountId == id);
            var toAcc = _bankAppDataContext.Accounts.FirstOrDefault(a => a.AccountId == accountNumber);

            if (fromAcc == null || toAcc == null)
                return RespCode.InvalidAccountNumber;

            if (fromAcc.Balance < amount)
                return RespCode.BalanceTooLow;

            fromAcc.Balance -= amount;
            toAcc.Balance += amount;

            _bankAppDataContext.SaveChanges();
            return RespCode.OK;
        }

    }
}
