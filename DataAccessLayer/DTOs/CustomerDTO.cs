namespace DataAccessLayer.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string? SocialSecurityNumber { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerCountry { get; set; }
        public GenderEnum CustomerGender { get; set; }
        public string CustomerCountryCode { get; set; }
        public DateOnly? CustomerBirthDate { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerPhoneCode { get; set; }
        public string CustomerPostalCode { get; set; }
        public string CustomerEmail { get; set; }
        public bool IsActive { get; set; }
    }
}



