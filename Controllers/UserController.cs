using MASHROEE.Models;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;

namespace MASHROEE.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly UserManager<Applicationuser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserController(UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var userFDb = await userManager.FindByIdAsync(id);
            if (userFDb == null)
            {
                return NotFound("user not found");
            }
            UserViewModel uservm = new UserViewModel()
            {
                id = userFDb.Id,
                fullname = userFDb.Name,
                UserName = userFDb.UserName,
                phone = userFDb.PhoneNumber,
                Email = userFDb.Email,
                image = userFDb.imageurl
            };
            return View(uservm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel newuser,string id, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                var userfDb = await userManager.FindByIdAsync(id);
                if (userfDb == null)
                {
                    return NotFound("user not found");
                }
                userfDb.UserName = newuser.UserName;
                userfDb.Name = newuser.fullname;
                userfDb.PhoneNumber = newuser.phone;
                userfDb.Email = newuser.Email;
                if (image != null && image.Length > 0)
                {
                    var fullpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);
                    using (var stream = new FileStream(fullpath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    userfDb.imageurl = "/images/" + image.FileName;
                }
                else
                {
                    userfDb.imageurl = userfDb.imageurl;
                }
                var result = await userManager.UpdateAsync(userfDb);

                if (result.Succeeded)
                {
                    TempData["Message"] = "user updated successfully!";
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                return View("Update");
                }
            }
            return View(newuser);
        }

        public async Task<IActionResult> Delete(string username)
        {
          var userfDb= await userManager.FindByNameAsync(username);
            if (username!=null)
            {
                
            if (userfDb == null)
                return Content("user not found!");
            IdentityResult res =await userManager.DeleteAsync(userfDb);
            if (res.Succeeded)
                return RedirectToAction("index", "DashBoard");
            else
            {
                foreach(var error in res.Errors)
                    ModelState.AddModelError(string.Empty,error.Description);
            }
            }
            return View("index", "DashBoard");
        }
        [HttpGet]
        public IActionResult addrole_forUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return Content("Username is required.");
            }

            ViewBag.Username = username;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addrole_forUser(string username, string rolename)
        {
        
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Content("User not found.");
            }

          
            var findrole = roleManager.Roles.Any(r => r.Name == rolename);
            if (!findrole)
            {
                return Content("Role not found.");
            }

          
            var result = await userManager.AddToRoleAsync(user, rolename);
            if (result.Succeeded)
            {
                return RedirectToAction("index","DashBoard");
            }

            return Content("Failed to add role.");
        }
        [HttpGet]
        public async Task< IActionResult >deleterole_fromUser(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            ViewBag.Username = username;
            ViewBag.roles = await userManager.GetRolesAsync(user);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> deleterole_fromUser(string username, string rolename)
        {
            var user=await userManager.FindByNameAsync(username);
            IdentityResult res = await userManager.RemoveFromRoleAsync(user,rolename);
            if (res.Succeeded)
            {
                return RedirectToAction("index", "DashBoard");
            }
            else
            {
                foreach (var error in res.Errors)
                    Console.WriteLine(error.Description);
            }
            return View("index", "DashBoard");
        }





    }
}
