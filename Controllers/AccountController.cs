using MASHROEE.Models;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;

namespace MASHROEE.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Applicationuser> userManager;
        private readonly SignInManager<Applicationuser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<Applicationuser> userManager, SignInManager<Applicationuser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public IActionResult index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel uservm, IFormFile  imagefromuser)
        {
            if (ModelState.IsValid)
            {
                Applicationuser user = new Applicationuser()
                {
                    UserName = uservm.username,
                    Name = uservm.Name,
                    Gender = uservm.Gender,
                    Email = uservm.Email,
                    PasswordHash = uservm.Password,
                    PhoneNumber = uservm.Phone,
                    
                };
                if (imagefromuser != null && imagefromuser.Length > 0)
                {
                    // fullpath= (path for project) + /wwwroot/images/ + image name===>Fullpath for image
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imagefromuser.FileName);
                    // copy image to WWWroot on fullpath
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await imagefromuser.CopyToAsync(stream);
                    }
                    user.imageurl = "/images/" + imagefromuser.FileName;

                }
                IdentityResult res = await userManager.CreateAsync(user, uservm.Password);
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, uservm.RoleName);
                    await signInManager.SignInAsync(user, false);
					TempData["LoginSuccess"] = "Sign in successful! Welcome to MASHROEE.";
					return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var erorr in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, erorr.Description);
                    }
                }

            }

            return View(uservm);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel uservm)
        {
            if (ModelState.IsValid)
            {
                Applicationuser user = await userManager.FindByNameAsync(uservm.username);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, uservm.password);
                    if (found)
                    {
                        await signInManager.SignInAsync(user, uservm.RememberMe);
						TempData["LoginSuccess"] = "Login successful! Welcome to MASHROEE.";
						return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Password Wrong");
                    }
                }
                ModelState.AddModelError("", "Username  Wrong");
            }

                return View(uservm);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["LogoutSuccess"] = "Log Out successful!";
            return RedirectToAction("Login");
        }

        [Authorize]
         public async Task<IActionResult> Profile()
         {
                var userId = userManager.GetUserId(User);
                var user = await userManager.FindByIdAsync(userId);
            var userrole =await  userManager.GetRolesAsync(user);
            //if(userrole==null)
            //    return NotFound("role not found");
            //var rolename = userrole.FirstOrDefault();
                if (user == null)
                {
                    return RedirectToAction("Login");
                }
            ProfileViewModel profileviewmodel = new ProfileViewModel();
                profileviewmodel.name = user.Name;
                profileviewmodel.username = user.UserName;
                profileviewmodel.Email = user.Email;
                profileviewmodel.Phone = user.PhoneNumber;
                profileviewmodel.imagurl = user.imageurl;
                profileviewmodel.Roles = userrole;
                return View(profileviewmodel);
        }
        [HttpGet]
        [Authorize]
        //get Data for user from database and return old data to Update
        public async Task<IActionResult> EditProfile()
        {
            var userFDb=await userManager.GetUserAsync(User);
            if (userFDb == null)
            {
                return RedirectToAction("login");
            }
            ProfileViewModel uservm = new ProfileViewModel()
            {
                name = userFDb.Name,
                username = userFDb.UserName,
                Phone = userFDb.PhoneNumber,
                Email = userFDb.Email,
                imagurl = userFDb.imageurl
            };
            return View(uservm);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileViewModel newuser,IFormFile ?imagurl)
        {
            if (ModelState.IsValid)
            {
                var userfDb= await userManager.GetUserAsync(User);
                userfDb.UserName=newuser.username;
                userfDb.Name=newuser.name;
                userfDb.PhoneNumber = newuser.Phone;
                userfDb.Email= newuser.Email;
                if (imagurl != null && imagurl.Length > 0)
                {
                    var fullpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imagurl.FileName);
                    using (var stream = new FileStream(fullpath, FileMode.Create))
                    {
                        await imagurl.CopyToAsync(stream);
                    }
                    userfDb.imageurl = "/images/" + imagurl.FileName;
                }
                else
                {
                    userfDb.imageurl = userfDb.imageurl;
                }
                var result = await userManager.UpdateAsync(userfDb);

                if (result.Succeeded)
                {
                    TempData["Message"] = "Profile updated successfully!";
                    return RedirectToAction("Profile");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("Profile");
            }
            return View(newuser);
        }

    }
}
