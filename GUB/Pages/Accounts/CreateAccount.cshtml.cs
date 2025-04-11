using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.BusinessLogic.AccountManagement;
using System.ComponentModel.DataAnnotations;

namespace GUB.Pages.Accounts
{
    [BindProperties]
    public class CreateAccountModel : PageModel
    {
        private readonly IAccountService _accountService;

        public CreateAccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public int CustomerId { get; set; }
        [Range(1, 99, ErrorMessage = "Choose a valid Frequency!")]
        public Frequency Frequency { get; set; }
        public List<SelectListItem> Frequencies { get; set; }
        [DataType(DataType.Date)]
        public string CreatedInput { get; set; }
        public DateOnly Created { get; set; }
        [Range(typeof(decimal), "100", "10000", ErrorMessage = "Amount must be between 100 and 10000.")]
        public decimal Balance { get; set; }
        [Range(1, 99, ErrorMessage = "Choose a valid Type!")]
        public TypeEnum Type { get; set; }
        public List<SelectListItem> Types { get; set; }

        public void OnGet(int id)
        {
            CustomerId = id;
            Frequencies = _accountService.FillFrequency();
            Types = _accountService.FillType();
        }

        //public IActionResult OnPost()
        //{

        //}
    }
}
