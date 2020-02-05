using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Doggy.Learning.WebService.Models
{
    public class AuthenticateRequestBody
    {
        [Required]
        [FromBody]
        public string Username { get; set; }

        [Required]
        [FromBody]
        public string Password { get; set; }
    }
}