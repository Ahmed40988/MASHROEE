using MASHROEE.IRepository;
using MASHROEE.Models;
using MASHROEE.Repository;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MASHROEE.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly UserManager<Applicationuser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public DashBoardController( IProductRepository productRepository,UserManager<Applicationuser> userManager,RoleManager<IdentityRole> roleManager) 
        {
            this.productRepository = productRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
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
            return View(modellist);
        }

       
    }
}
