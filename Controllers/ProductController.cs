using MASHROEE.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MASHROEE.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        public ProductController(IProductRepository productRepository,ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}
