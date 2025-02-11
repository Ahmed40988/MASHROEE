using MASHROEE.IRepository;
using MASHROEE.Models;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System.Security.Claims;

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
            return View(list);
        }

        public IActionResult Search(string searchBy, string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return Json(new { success = false, message = "Please enter a name or price for search." });
            }
            if (string.IsNullOrEmpty(searchBy))
            {
                return Json(new { success = false, message = "Please select search by name or search by Price!." });
            }

            if (searchBy.ToLower() == "productname")
            {
                var list1 = productRepository.Searchbyname(searchValue);
                var list = productRepository.Maping(list1);
                return View(list);
            }
            else if (searchBy.ToLower() == "price")
            {
                if (decimal.TryParse(searchValue, out decimal price))
                {
                    var list1 = productRepository.Searchbyprice(price);
                    var list = productRepository.Maping(list1);
                    return View(list);
                }
                else
                {
              
                    return Json(new { success = false, message = "Invalid price format. Please enter a valid number." });
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int id)
        {
           var product= productRepository.GetProductById(id);
            if(product==null)
                return NotFound();

            return View(product);
        }

        [HttpGet]
        [Authorize(Roles = "admin,Supplier")]
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
                        Quantity = newp.Quantity,
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
        [Authorize(Roles = "admin,Supplier")]
        public IActionResult Edit(int id)
        {
            var listcategory=categoryRepository.Getselectlist();
            var product = productRepository.GetProductById(id) ;
            var model = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity= product.Quantity,
                CreatedDate = product.CreatedDate,
                image= product.image,
                categories=listcategory,
                CategoryName = product.category?.Name ?? "No Category"
            };
            return View(model);
        }
        [HttpPost]  
        public async Task<IActionResult> Edit(int id,ProductViewModel newp,IFormFile?image)
        {
            ModelState.Remove("image");
            if (ModelState.IsValid)
            {
                var productDB= productRepository.GetProductById(id);
                var category=categoryRepository.GetCategoryByName(newp.CategoryName);
           
                productDB.Name = newp.Name;
                productDB.Description = newp.Description;
                productDB.Price = newp.Price;
                productDB.Quantity = newp.Quantity;
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
               await productRepository.UpdateProductAsync(productDB);
                return RedirectToAction("index","Home");
            }
            return View(newp);
        }
        [Authorize(Roles = "admin,Supplier")]
        public ActionResult Delete(int id)
        {
            productRepository.RemoveProduct(id);
            ViewBag.DeleteSuccess = "Product deleted successfully!";
            return RedirectToAction("index", "Home");
        }
        [Authorize]
        [Authorize(Roles = "admin,Supplier")]
        public IActionResult MyProducts()
        {
            var userid=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userid != null)
            {
                var products = productRepository.GetallProductsforuserid(userid);
                var modellist = productRepository.Maping(products);

                 return View(modellist);
            }
            return Content("Supllier is Not found");
        }

    }
}
