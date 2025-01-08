using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MASHROEE.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "username is Required")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public bool RememberMe { get; set; }

    }
}
