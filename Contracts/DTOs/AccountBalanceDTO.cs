using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class AccountBalanceDTO
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public DateTime TransactionDate { get; set; }
        public int CustomerId { get; set; }
    }
}
