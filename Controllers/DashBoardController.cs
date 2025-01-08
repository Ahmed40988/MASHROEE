using MASHROEE.Models;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MASHROEE.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly UserManager<Applicationuser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public DashBoardController(UserManager<Applicationuser> userManager,RoleManager<IdentityRole> roleManager) 
        {
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
                UserViewModel model = new UserViewModel()
                {
                    UserName=user.UserName,
                    fullname=user.Name,
                    phone=user.PhoneNumber,
                   Email=user.Email,
                   Roles=roles,
                   image=user.imageurl
                };
                modellist.Add(model);
            }
            return View(modellist);
        }

       
    }
}
