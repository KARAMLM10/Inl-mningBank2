namespace InlämningBank2.ViewModel
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string? Gender { get; set; }
        public string? Givenname { get; set; }
        public DateTime? Birthday { get; set; }
        public string? NationalId { get; set; }
        public string? Telephonenumber { get; set; }
        public string? Emailaddress { get; set; }
        public string Country { get; set; } = null!;
    }
}
