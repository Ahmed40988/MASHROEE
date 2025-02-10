using MASHROEE.IRepository;
using MASHROEE.Models;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MASHROEE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;

        public HomeController(ILogger<HomeController> logger,ICategoryRepository categoryRepository,IProductRepository productRepository)
        {
            _logger = logger;
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
			if (TempData["LoginSuccess"] != null)
			{
				TempData.Keep("LoginSuccess"); 
			}
			var list = productRepository.GetAllProducts();
            var listmodel = new List<ProductViewModel>();
            foreach (var item in list)
            {
                var model = new ProductViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    CategoryName = item.category.Name,
                    Price = item.Price,
                    CreatedDate = item.CreatedDate,
                    image = item.image,
                };
                listmodel.Add(model);
            }
            return View(listmodel);
        }
        public IActionResult Categories() 
        {
            var list=categoryRepository.GetAllCategorys();
            return View(list);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
