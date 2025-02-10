using MASHROEE.Models;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MASHROEE.Component
{
    public class ProfileComponent : ViewComponent
    {
        private readonly UserManager<Applicationuser> userManager;

        public ProfileComponent(UserManager<Applicationuser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("Login", "Account");
            }

            var userrole = await userManager.GetRolesAsync(user);

            ProfileViewModel profileviewmodel = new ProfileViewModel
            {
                name = user.Name,
                username = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                imagurl = user.imageurl,
                Roles = userrole
            };

            return View(profileviewmodel);
        }
    }
}