
using System.ComponentModel.DataAnnotations;

public enum Frequency
{
    [Display(Name = "Choose...")]
    Choose,
    Monthly,
    Weekly,
    [Display(Name = "After-Transaction")]
    AfterTransaction,
}


