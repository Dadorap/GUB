using DataAccessLayer.DTOs;
using Newtonsoft.Json.Linq;

namespace GUB.API;

public class ZenQuotesService
{

    private readonly HttpClient _httpClient;

    public ZenQuotesService()
    {
        _httpClient = new HttpClient();
    }


    public async Task<List<ZenQuotesDTO>> GetQuotes()
    {

        try
        {
            string response = await _httpClient.GetStringAsync("https://zenquotes.io/api/random");
            JArray json = JArray.Parse(response);

            List<ZenQuotesDTO> quotesList = json.Select(q => new ZenQuotesDTO
            {
                Quote = q["q"]?.ToString(),
                Author = q["a"]?.ToString()
            }).ToList();

            return quotesList;

        }
        catch
        {
            var fallbackQuotes = new List<(string quote, string author)>
        {
            ("The best way to predict the future is to create it.", "Peter Drucker"),
            ("Happiness depends upon ourselves.", "Aristotle"),
            ("Do what you can, with what you have, where you are.", "Theodore Roosevelt"),
            ("Everything you can imagine is real.", "Pablo Picasso"),
            ("All religions, arts and sciences are branches of the same tree.", "Albert Einstein"),
            ("Values are like fingerprints. Nobody's are the same, but you leave 'em all over everything you do."
            , "Elvis Presley"),
            ("The moment you doubt whether you can fly, you cease for ever to be able to do it."
            , "James Matthew Barrie"),
            ("A clever person turns great troubles into little ones, and little ones into none at all. "
            , "Chinese Proverb"),
            ("Enjoy every minute of life. Never second-guess life."
            , "Michael Jordan"),
            ("You can start changing our world for the better daily, no matter how small the action."
            , "Nelson Mandela"),
            ("Don't let someone elses. opinion become your reality."
            , "Les Brown"),
        };

            var random = new Random();
            var randomQuote = fallbackQuotes[random.Next(fallbackQuotes.Count)];

            return new List<ZenQuotesDTO>
            {
                new ZenQuotesDTO
                {
                    Quote = randomQuote.quote,
                    Author = randomQuote.author
                }
            };

        }
    }

}
