using System.ComponentModel.DataAnnotations;

public enum GenderEnum
{
    [Display(Name = "Choose...")]
    Choose = 0,

    [Display(Name = "Male")]
    Male = 1,

    [Display(Name = "Female")]
    Female = 2,

    [Display(Name = "Other")]
    Other = 3,

    [Display(Name = "No-Answer")]
    NoAnswer = 99
}


public enum CountryCode
{
    Choose,
    SE,
    FI,
    DK,
    NO
}

public enum CustomerCountry
{
    Choose,
    Sweden,
    Finland,
    Denmark,
    Norway
}

public enum PhoneCode
{
    None,
    Denmark = 45,
    Sweden = 46,
    Norway = 47,
    Finland = 358
}




public enum RespCode
{
    OK,
    BalanceTooLow,
    IncorrectAmount,
    InvalidMessage,
    InvalidDate,
    InvalidCountry
}
namespace DataAccessLayer.Enums
{
    internal class Enums
    {
    }
}
