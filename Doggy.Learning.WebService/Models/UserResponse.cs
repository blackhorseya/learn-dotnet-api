using System.Collections.Generic;

namespace Doggy.Learning.WebService.ViewModels
{
    public class UserResponse
    {
        public string Name { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
    }
}