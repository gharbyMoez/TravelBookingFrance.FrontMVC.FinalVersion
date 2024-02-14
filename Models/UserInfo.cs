namespace TravelBookingFrance.FrontMVC.Models
{
    public class UserInfo
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Address { get; set; }

        public string? City { get; set; }

        public string? Prov { get; set; }

        public string? Postal { get; set; }

        public string? Country { get; set; }

        public string? HomePhone { get; set; }

        public string? BusPhone { get; set; }

        public string? Email { get; set; }
    }
}
