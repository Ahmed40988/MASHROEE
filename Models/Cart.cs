namespace MASHROEE.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Cartitem> CartItems { get; set; }
    }
}
