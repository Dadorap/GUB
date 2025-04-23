using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BusinessLogic.About
{
    public class AboutService : IAboutService
    {
        private readonly BankAppDataContext _bankAppDataContext;

        public AboutService(BankAppDataContext bankAppDataContext)
        {
            _bankAppDataContext = bankAppDataContext;
        }

        public AboutDTO GetData()
        {
            var aboutInfo = new AboutDTO
            {
                Customers = _bankAppDataContext.Customers
                            .Where(c => c.IsActive)
                            .Count(),

                Accounts = _bankAppDataContext.Accounts
                           .Where(a => a.IsActive)
                           .Count(),

                Transactions = _bankAppDataContext.Transactions
                                .Count(),

                Balance = _bankAppDataContext.Accounts
                         .Where(a => a.IsActive)
                         .Sum(a => (decimal?)a.Balance) ?? 0

            };

            return aboutInfo;
        }
    }
}
