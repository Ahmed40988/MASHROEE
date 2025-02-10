using System.ComponentModel.DataAnnotations;

namespace MASHROEE.ViewModel
{
   
    public class CategoryViewModel
    {
            public int Id { get; set; }

            [Required]
            [Display(Name = "Category Name"), MaxLength(100), MinLength(3)]
            public string Name { get; set; } = string.Empty;


            [StringLength(500), MinLength(3)]
            public string Description { get; set; } = string.Empty;

            public List<ProductViewModel>? Products { get; set; }

            public int ProductsCount;
        }
}
