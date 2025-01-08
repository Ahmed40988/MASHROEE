namespace MASHROEE.ViewModel
{
    public class ProfileViewModel
    {
        public string ?id { get; set; }  
        public string username { get; set; }
        public string name { get; set; }
        public string Email { get; set; }
        public string Phone {  get; set; }
        public IList<string> ?Roles {  get; set; }
        public string ?imagurl {  get; set; }
    }
}
