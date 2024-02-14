namespace TravelBookingFrance.FrontMVC.Models
{
    public class ProductDTO
    {
        public string ProdName { get; set; }
        public string ProdDescription { get; set; }
        //  public virtual ICollection<PhotoDTO>? Photos { get; set; }
        public virtual ICollection<string>? Photos { get; set; }
    }
}