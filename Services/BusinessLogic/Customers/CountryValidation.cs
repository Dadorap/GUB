namespace Services.BusinessLogic.Customers;

public class CountryValidation
{
    private readonly Dictionary<CountryCode, string> CountryNames = new()
{
    { CountryCode.SE, "Sweden" },
    { CountryCode.FI, "Finland" },
    { CountryCode.DK, "Denmark" },
    { CountryCode.NO, "Norway" }
};

    public RespCode ValidateCountryCodeAndName(string codeInput, string countryInput)
    {
        if (!Enum.TryParse<CountryCode>(codeInput.Trim().ToUpperInvariant(), out var parsedCode))
            return RespCode.InvalidCountry;

        if (!CountryNames.TryGetValue(parsedCode, out var expectedCountry))
            return RespCode.InvalidCountry;

        if (!string.Equals(expectedCountry, countryInput.Trim(), StringComparison.OrdinalIgnoreCase))
            return RespCode.InvalidCountry;

        return RespCode.OK;
    }

}
