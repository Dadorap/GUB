using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.BusinessLogic.MoneyLaundering
{
    public class SuspiciousTransactionChecker
    {
        private readonly BankAppDataContext _bankAppDataContext;

        public SuspiciousTransactionChecker(BankAppDataContext bankAppDataContext)
        {
            _bankAppDataContext = bankAppDataContext;
        }

        public List<Transaction> GetSuspiciousTransactionsForUser(int customerId)
        {
            var suspicious = new List<Transaction>();
            var transactions = _bankAppDataContext.Transactions
                .Include(t => t.AccountNavigation)
                .Where(t => t.AccountNavigation.Dispositions.Any(d => d.CustomerId == customerId))
                .ToList();
            Console.WriteLine($"Checking customer {customerId}. Transactions found: {transactions.Count}");

            foreach (var t in transactions)
            {
                Console.WriteLine($"  - TxId {t.TransactionId}, Amount {t.Amount}, Date {t.Date}");

                if (t.Amount > 15000)
                {
                    suspicious.Add(t);
                    continue;
                }

                var fromTime = t.Date.ToDateTime(TimeOnly.MinValue).AddHours(-72);
                var toTime = t.Date.ToDateTime(TimeOnly.MaxValue);

                var sum72h = transactions
                    .Where(t => t.Date.ToDateTime(TimeOnly.MinValue) >= fromTime &&
                                t.Date.ToDateTime(TimeOnly.MinValue) <= toTime)
                    .Sum(t => t.Amount);

                if (sum72h > 23000)
                {
                    suspicious.Add(t);
                }
            }

            return suspicious;
        }
    }

}
