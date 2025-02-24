using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MASHROEE.ViewModel
{
    public class ProductViewModel
    {  
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is Required!")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Category is Required!")]
        public string CategoryName { get; set; }
        public IEnumerable<SelectListItem> categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000 Dollar $.")]
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ?image {  get; set; }
        public string ?username {  get; set; }
        [Range(1,100,ErrorMessage= "Quantity must be between 1 and 100.")]
        public int? Quantity { get; set; }

    }
}
