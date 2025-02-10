namespace MASHROEE.Models
{
    public class Cartitem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set;}

        public string ?image {  get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price;
        public Cart Cart { get; set; }
    }
}
