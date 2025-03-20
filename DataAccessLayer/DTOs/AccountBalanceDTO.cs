using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class AccountBalanceDTO
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
    }
}
