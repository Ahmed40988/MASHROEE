using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MASHROEE.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="username is Required")]
        public string username { get; set; }
        [Required(ErrorMessage = "username is Required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Phone {  get; set; }
        public IFormFile imagefromuser { get; set; }
        public char Gender { get; set; }
        [Required(ErrorMessage="Role is Required")]
        public string RoleName  { get; set; }
    }
}
