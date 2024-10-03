namespace Finnce_Api.Models.UserModel
{
    public class UserModelCreate
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
