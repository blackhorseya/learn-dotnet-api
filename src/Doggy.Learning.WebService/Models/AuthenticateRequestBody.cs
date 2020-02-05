using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

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

    public class AuthenticateRequestBodyExample : IExamplesProvider<AuthenticateRequestBody>
    {
        public AuthenticateRequestBody GetExamples()
        {
            return new AuthenticateRequestBody
            {
                Username = "pls",
                Password = "123"
            };
        }
    }
    
    public class  AuthenticateResponseExample : IExamplesProvider<IDictionary<string, string>>
    {
        public IDictionary<string, string> GetExamples()
        {
            return new Dictionary<string, string>
            {
                {
                    "token",
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InBscyIsIm5hbWVpZCI6IjEiLCJyb2xlIjoiYWRtaW4iLCJuYmYiOjE1ODA5MDI5MTMsImV4cCI6MTU4MDkxMDExMywiaWF0IjoxNTgwOTAyOTEzfQ.hv2hmW494K87URlgcBNTYSBZsAEc5QWXWWtnwDZa8UE"
                }
            };
        }
    }
}