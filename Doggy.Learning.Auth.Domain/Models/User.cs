namespace Doggy.Learning.Auth.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }

    public static class Extensions
    {
        public static User WithoutPassword(this User user)
        {
            if (user == null) return null;
            
            user.Password = null;
            return user;
        }
    }
}