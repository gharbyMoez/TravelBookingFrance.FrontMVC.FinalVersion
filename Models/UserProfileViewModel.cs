namespace TravelBookingFrance.FrontMVC.Models
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public UserInfo UserProfile { get; set; }
        public List<TravelInfo> TravelList { get; set; }
    }
}