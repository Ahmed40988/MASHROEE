using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MASHROEE.Models
{
    public class Applicationuser:IdentityUser
    {
        
        public string Name { get; set; }
        public char Gender { get; set; }
        public string ?imageurl { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
