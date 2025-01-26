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
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ?image {  get; set; }

    }
}
