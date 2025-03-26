namespace Services.BusinessLogic.Validations;

public class CountryValidation : ICountryValidation
{
    private readonly Dictionary<CountryCode, CustomerCountry> CountryNames = new()
{
    { CountryCode.SE, CustomerCountry.Sweden },
    { CountryCode.FI, CustomerCountry.Finland },
    { CountryCode.DK, CustomerCountry.Denmark },
    { CountryCode.NO, CustomerCountry.Norway }
};

    public RespCode ValidateCountryCodeAndName(CountryCode codeInput, CustomerCountry countryInput)
    {
        if (!CountryNames.TryGetValue(codeInput, out var expectedCountry))
            return RespCode.InvalidCountry;

        if (expectedCountry != countryInput)
            return RespCode.InvalidCountry;

        return RespCode.OK;
    }


}
