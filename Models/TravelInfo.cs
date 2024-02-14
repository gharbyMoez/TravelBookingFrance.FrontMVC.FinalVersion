namespace TravelBookingFrance.FrontMVC.Models
{
    public class TravelInfo
    {
        public int TravelId { get; set; }
        public string? TravelDate { get; set; }
        public string TravelName { get; set; }
        public string Destination { get; set; }
        public string? DepartureDate { get; set; }
        public string DepartureAirport { get; set; }
        public string urlTravelImage { get; set; }
        //product urlTravelImage
        //public List<string> ProdName { get; set; }  //
        //public string ProdDescription { get; set; }
        public virtual ICollection<ActivityDTO>? Activities { get; set; }
        public virtual ICollection<ProductDTO>? Products { get; set; }

        //TripeType
        public string Ttname { get; set; }

        //Flight
        public string FlightNumber { get; set; }
        public string CompanyName { get; set; }
        public DateTime DateOfDepart { get; set; }
        public DateTime DateOfArrival { get; set; }
        public string SeatNumber { get; set; }
    }
}
