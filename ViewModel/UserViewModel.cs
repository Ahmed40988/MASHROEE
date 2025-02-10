using MASHROEE.Models;

namespace MASHROEE.ViewModel
{
    public class UserViewModel
    {
        public string id { get; set; }
        public string fullname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public string ?image { get; set; }
        public IList<string>? Roles  { get; set; }

       public ICollection<Product> ?products { get; set; } 

    }
}
