using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.BusinessLogic.MoneyLaundering;


namespace MoneyLaundering
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Money laundering scanner running...");

            var options = new DbContextOptionsBuilder<BankAppDataContext>()
                .UseSqlServer("Server=localhost;Database=BankAppData;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true")
                .Options;

            using var context = new BankAppDataContext(options);

            var checker = new SuspiciousTransactionChecker(context);
            var writer = new ReportWriter();

            //var countries = context.Customers
            //    .Select(c => c.Country)
            //    .Distinct()
            //    .ToList();           

            var countries = new List<string>() { "finland" };


            foreach (var country in countries)
            {
                Console.WriteLine($"Scanning country: {country}");
                Console.WriteLine($"Please wait, it might take few minutes...");

                var customers = context.Customers
                                    .Where(c => c.Country == country && c.IsActive)
                                    .ToList();


                var reportData = new List<(Customer, Account, Transaction)>();

                foreach (var customer in customers)
                {
                    var suspicious = checker.GetSuspiciousTransactionsForUser(customer.CustomerId);

                    if (suspicious.Count > 0)
                    {

                        var accountIds = suspicious.Select(t => t.AccountId).Distinct().ToList();

                        var accountMap = context.Accounts
                            .Where(a => accountIds.Contains(a.AccountId))
                            .ToDictionary(a => a.AccountId);

                        foreach (var trans in suspicious)
                        {
                            if (!writer.AlreadyReported(trans.TransactionId))
                            {
                                var account = accountMap[trans.AccountId];

                                reportData.Add((customer, account, trans));
                                writer.SaveAsReported(trans.TransactionId);
                                Console.WriteLine("Suspicious transactions");
                                Console.WriteLine($"Account Id: {trans.AccountId}, Ammount: {trans.Amount}");
                            }
                        }
                    }
                }

                if (reportData.Any())
                {
                    writer.WriteReport(country, reportData);
                    Console.WriteLine($"✓ Report written for {country} with {reportData.Count} entries.");
                }
                else
                {
                    Console.WriteLine($"✕ No suspicious transactions found for {country}.");
                }
            }

            Console.WriteLine("Done. Press any key to exit...");
            Console.ReadKey();

        }
    }
}







