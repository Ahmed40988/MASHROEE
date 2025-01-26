using MASHROEE.IRepository;
using MASHROEE.Models;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;

namespace MASHROEE.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly UserManager<Applicationuser> userManager;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, UserManager<Applicationuser> userManager)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
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

        [HttpGet]
        public IActionResult Create()
        {
            ProductViewModel model = new ProductViewModel()
            {
                categories = categoryRepository.Getselectlist()

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel newp, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var category = categoryRepository.GetCategoryByName(newp.CategoryName);
                if (category != null)
                {
                    var product = new Product()
                    {
                        Name = newp.Name,
                        Description = newp.Description,
                        Price = newp.Price,
                        CreatedDate = DateTime.Now,
                        categoryid = category.Id,
                        userid = userManager.GetUserId(User),
                    };
                    if (image != null && image.Length > 0)
                    {
                        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        product.image = "/images/" + image.FileName;
                    }
                    productRepository.AddProduct(product);
                    return RedirectToAction("Index");
                }
                return Content("Category is not found");
            }
            newp.categories = categoryRepository.Getselectlist();
            return View(newp);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.listcategory=categoryRepository.GetAllCategorys();
            var product = productRepository.GetProductById(id) ;
            var model = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedDate = product.CreatedDate,
                image= product.image,
                CategoryName = product.category?.Name ?? "No Category"
            };
            return View(model);
        }
        [HttpPost]  
        public async Task<IActionResult> Edit(int id,ProductViewModel newp,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var productDB= productRepository.GetProductById(id);
                var category=categoryRepository.GetCategoryByName(newp.CategoryName);
           
                productDB.Name = newp.Name;
                productDB.Description = newp.Description;
                productDB.Price = newp.Price;
                productDB.CreatedDate = newp.CreatedDate;
                productDB.categoryid =category.Id;
                if (image != null && image.Length > 0)
                {
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    productDB.image = "/images/" + image.FileName;
                }
                else
                {
                    productDB.image = productDB.image;
                }
                productRepository.updateproduct(productDB);
                return RedirectToAction("Index");
            }
            return View(newp);
        }

        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                productRepository.RemoveProduct(id);
                return RedirectToAction("Index");
            }
            return Content("Produt is not Deleted!");
        }


    }
}
