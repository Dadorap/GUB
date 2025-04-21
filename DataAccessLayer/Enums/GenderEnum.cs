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
