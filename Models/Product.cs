namespace MASHROEE.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? userid { get; set; }

        public Applicationuser user { get; set; }

        public int? categoryid { get; set; }

        public Category category { get; set; }

        public string? image { get; set; }
        public int? Quantity { get; set; }
    }
}
