using DataAccessLayer.Models;


namespace Services.BusinessLogic.MoneyLaundering
{
    public class ReportWriter
    {
        private const string ReportedFilePath = "reported_ids.txt";
        private static readonly string ReportsFolder = Path.Combine(AppContext.BaseDirectory, "../../../Reports");

        public void WriteReport(string country, List<(Customer customer, Account account, Transaction transaction)> findings)
        {
            Directory.CreateDirectory(ReportsFolder);

            var path = Path.Combine(ReportsFolder, $"Suspicious_{country}_{DateTime.Now:yyyyMMdd_HHmm}.txt");

            using var writer = new StreamWriter(path);
            foreach (var (customer, account, transaction) in findings)
            {
                writer.WriteLine($"Customer: {customer.Givenname} {customer.Surname}, Account: {account.AccountId}, Transaction: {transaction.TransactionId}");
            }
        }

        public bool AlreadyReported(int transactionId)
        {
            if (!File.Exists(ReportedFilePath))
                return false;

            return File.ReadAllLines(ReportedFilePath)
                       .Contains(transactionId.ToString());
        }

        public void SaveAsReported(int transactionId)
        {
            File.AppendAllText(ReportedFilePath, transactionId + Environment.NewLine);
        }
    }
}


