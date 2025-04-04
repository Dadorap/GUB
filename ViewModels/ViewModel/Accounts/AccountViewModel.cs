using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModel.Accounts
{
    public class AccountViewModel
    {

        public string Frequency { get; set; } = null!;

        public DateOnly Created { get; set; }

        public decimal Balance { get; set; }
    }
}
