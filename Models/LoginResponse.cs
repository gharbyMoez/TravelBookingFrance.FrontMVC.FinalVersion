namespace TravelBookingFrance.FrontMVC.Models
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsAuthenticated { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
