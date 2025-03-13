using DataAccessLayer.DTOs;
using GUB.API;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace GUB.Pages;

public class IndexModel : PageModel
{
    private readonly ZenQuotesService _zenQuotesService;

    public IndexModel(ZenQuotesService zenQuotesService)
    {
        _zenQuotesService = zenQuotesService;
    }


    public List<ZenQuotesDTO> ZenQuotes { get; set; } = new List<ZenQuotesDTO>();
    public string DateOnly { get; set; }

    public async Task OnGet()
    {
        ZenQuotes = await _zenQuotesService.GetQuotes(); DateTime now = DateTime.Now;
        DateOnly = now.ToString("dddd, MMMM d 'at' HH:mm", CultureInfo.InvariantCulture);


    }
}
