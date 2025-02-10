using MASHROEE.IRepository;
using MASHROEE.Models;
using MASHROEE.Repository;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MASHROEE.Controllers
{
    [Authorize(Roles = "admin")]
    public class DashBoardController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly UserManager<Applicationuser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
		private readonly ICartRepository cartRepository;

		public DashBoardController( IProductRepository productRepository,ICategoryRepository categoryRepository
            ,UserManager<Applicationuser> userManager,RoleManager<IdentityRole> roleManager,ICartRepository cartRepository) 
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
			this.cartRepository = cartRepository;
		}

        public IActionResult Index()
        {
       
            ViewData["HideFooter"] = true;
            return View();

        }

        public async Task<IActionResult >Users()
        {
            var list=userManager.Users.ToList();
            var modellist = new List< UserViewModel>();
            foreach (var user in list)
            {
                var roles=await userManager.GetRolesAsync(user);
                var products_users=productRepository.GetallProductsforuserid(user.Id).ToList();
                UserViewModel model = new UserViewModel()
                {
                    id = user.Id,
                    UserName=user.UserName,
                    fullname=user.Name,
                    phone=user.PhoneNumber,
                   Email=user.Email,
                   Roles=roles,
                   image=user.imageurl, 
                   products=products_users,
                };
                modellist.Add(model);
            }

            return PartialView("_Users",modellist);
        }

        public IActionResult Products()
        {
            var listp = productRepository.GetAllProducts();
            var listmodel = new List<ProductViewModel>();
            foreach (var item in listp)
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
                    username=item.user.Name,
                    Quantity = item.Quantity,
                };
                listmodel.Add(model);
            }
            return PartialView(listmodel);
        }
        public IActionResult Categories()
        {
            var listc = categoryRepository.GetAllCategorys();
            var listmodel = new List<CategoryViewModel>();
            foreach (var item in listc)
            {
                var model = new CategoryViewModel();
                model.Id = item.Id;
                model.Name = item.Name;
                model.Description = item.Description;
                model.ProductsCount=item.Products.Count;
                listmodel.Add(model);
            }
           var sorted =listmodel.OrderBy(c=>c.ProductsCount).ToList();
            return PartialView(sorted);
        }

        public IActionResult Roles() {
            var listrole = roleManager.Roles.ToList();
            List<RoleViewModel> roleViewModellist = new List<RoleViewModel>();
            foreach (var role in listrole)
            {
                RoleViewModel model = new RoleViewModel();
                model.RoleName = role.Name;
                roleViewModellist.Add(model);
            }
            return PartialView(roleViewModellist);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return PartialView("_AddRole");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel newrole)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = newrole.RoleName;
                var checkrol = await roleManager.FindByNameAsync(role.Name);
                if (checkrol == null)
                {
                    IdentityResult result = await roleManager.CreateAsync(role);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "DashBoard");
                    else
                    {
                        foreach (var error in result.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                    return RedirectToAction("index", "DashBoard");
                }
                else
                {
                    return Content("Role is added before");
                }
            }
            return Content("Error");
        }


        [HttpGet]
        public IActionResult CreateCategory()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var listc = categoryRepository.GetAllCategorys();
                if (listc.Any(c => c.Name == categoryViewModel.Name))
                {
                    TempData["CategoryExistsMessage"] = "Category is already added!"; // تخزين الرسالة في TempData
                    return View(categoryViewModel); // إعادة عرض النموذج مع الرسالة
                }

                var category = new Category()
                {
                    Name = categoryViewModel.Name,
                    Description = categoryViewModel.Description,
                };
                categoryRepository.AddCategory(category);
                return RedirectToAction("Index", "DashBoard");
            }
            return PartialView(categoryViewModel);
        }







    }
}
