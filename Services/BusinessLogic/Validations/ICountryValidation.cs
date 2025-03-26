namespace Services.BusinessLogic.Validations
{
    public interface ICountryValidation
    {
        RespCode ValidateCountryCodeAndName(CountryCode codeInput, CustomerCountry countryInput);
    }
}