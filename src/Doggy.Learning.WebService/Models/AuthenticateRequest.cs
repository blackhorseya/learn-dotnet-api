using System.ComponentModel.DataAnnotations;

namespace Doggy.Learning.WebService.Models
{
    public class AuthenticateRequest
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}