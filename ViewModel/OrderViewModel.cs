using MASHROEE.Models;

namespace MASHROEE.ViewModel
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public List<Cartitem> cartitems { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }


	}
}
