using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GUB.Pages
{
    [Authorize(Roles = "Cashier")]

    public class CustomerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
