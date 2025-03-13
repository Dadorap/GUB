using DataAccessLayer.DTOs;
using GUB.API;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GUB.Pages;

public class IndexModel : PageModel
{
    private readonly ZenQuotesService _zenQuotesService;

    public IndexModel(ZenQuotesService zenQuotesService)
    {
        _zenQuotesService = zenQuotesService;
    }


    public List<ZenQuotesDTO> ZenQuotes { get; set; } = new List<ZenQuotesDTO>();

    public async Task OnGet()
    {
        ZenQuotes = await _zenQuotesService.GetQuotes();
    }
}
