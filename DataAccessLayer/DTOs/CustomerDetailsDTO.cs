using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs;

public class CustomerDetailsDTO
{
    public int CustomerId { get; set; }
    public string CustomerGender { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerCity { get; set; }
    public string CustomerCountry { get; set; }
    public string CustomerCountryCode { get; set; }
    public DateOnly? CustomerBirthDate { get; set; }
    public string? SocialSecurityNumber { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerPhoneCode { get; set; }
    public string CustomerPostalCode { get; set; }
    public string CustomerEmail { get; set; }

    public decimal TotalBalance { get; set; }
    public List<AccountBalanceDTO> Accounts { get; set; } 

    public int Transactions { get; set; }
}
