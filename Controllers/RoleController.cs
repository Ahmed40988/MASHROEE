using MASHROEE.Models;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MASHROEE.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<Applicationuser> usermanager;

        public RoleController(RoleManager<IdentityRole>roleManager,UserManager<Applicationuser>usermanager)
        {
            this.roleManager = roleManager;
            this.usermanager = usermanager;
        }
        public IActionResult Index()
        {
            var listrole=roleManager.Roles.ToList();
           List< RoleViewModel> roleViewModellist = new List<RoleViewModel>();
            foreach (var role in listrole)
            {
           RoleViewModel model=new RoleViewModel();
                model.RoleName = role.Name;
                roleViewModellist.Add(model);
            }
            return View(roleViewModellist);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View("AddRole");
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel newrole)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = newrole.RoleName;
                var checkrol= await roleManager.FindByNameAsync(role.Name);
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

        public async Task<IActionResult> DeleteRole(string rolename)
        {
            var rolefDb=await roleManager.FindByNameAsync(rolename);
            if (rolefDb == null)
            {
                return Content("Error! role not found!");
            }
         IdentityResult res= await  roleManager.DeleteAsync( rolefDb);
            if (res.Succeeded)
                return RedirectToAction("index", "DashBoard");
            else
                return Content("Error! role is not Deleted!");
        }


    }
}
