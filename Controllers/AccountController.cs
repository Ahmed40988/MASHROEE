using MASHROEE.Models;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using MASHROEE.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MASHROEE.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Applicationuser> userManager;
        private readonly SignInManager<Applicationuser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
		private readonly IEmailSenderRepository emailSenderRepository;

		public AccountController(UserManager<Applicationuser> userManager, SignInManager<Applicationuser> signInManager,
            RoleManager<IdentityRole> roleManager, IEmailSenderRepository emailSenderRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
			this.emailSenderRepository = emailSenderRepository;
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

		public ActionResult ForgotPassword()
		{
			return View(); 
		}

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return Json(new { success = false, message = "Email id not found, please register or enter a valid email!" });
                }

                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);

                string emailBody = $@"
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
            color: #333;
            background-color: #f4f4f4; 
            padding: 20px;
        }}
        .container {{
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }}
        .button {{
            display: inline-block;
            padding: 12px 24px;
            margin: 20px 0;
            font-size: 16px;
            color: #fff;
            background-color: #007BFF; 
            text-decoration: none;
            border-radius: 5px;
        }}
        .button:hover {{
            background-color: #0056b3;
        }}
        .footer {{
            margin-top: 20px;
            font-size: 14px; 
            color: #555; 
        }}
        p {{
            font-size: 16px; 
            color: #333; 
        }}
        a {{
            color: #007BFF;
            text-decoration: none;
        }}
        a:hover {{
            text-decoration: underline;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <p>Hello {user.Name},</p>
        <p>We received a request to reset your account password. Please click the button below to reset your password:</p>
        <a href='{callbackUrl}' class='button'>Reset Password</a>
        <p>If you did not request a password reset, you can safely ignore this email.</p>
        <div class='footer'>
            <p>Best regards,</p>
            <p>Support Team</p>
        </div>
    </div>
</body>
</html>";

                await emailSenderRepository.SendEmailAsync(user.Email, "Password Reset Request", emailBody);

                return Json(new { success = true, message = "Password reset email sent successfully. Please Check Your Email To Reset Password!" });
            }

            return Json(new { success = false, message = "Invalid model state." });
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }



        public ActionResult ResetPassword(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return View("Error_for ResetePassword");
            }
            return View(new ResetPasswordViewModel { Code = code });
        }


        [HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model); 
			}
			var user = await userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				return RedirectToAction("ResetPassword", "Account");
			}
			var result = await userManager.ResetPasswordAsync(user, model.Code, model.Password);
			if (result.Succeeded)
			{
				return RedirectToAction("Login", "Account");
			}
            else
			{
                foreach (var item in result.Errors)
			   ModelState.AddModelError(string.Empty, item.Description);

			}
			return View(model);
		}


	}
}
