using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Doggy.Extensions.Http.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Doggy.Learning.WebService.Models
{
    public class AuthenticateRequestBody
    {
        [Required] [FromBody] public string Username { get; set; }

        [Required] [FromBody] public string Password { get; set; }
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

    public class AuthenticateSuccessResponseExample : IExamplesProvider<GenericHttpResponse>
    {
        public GenericHttpResponse GetExamples()
        {
            return new GenericHttpResponse
            {
                Code = StatusCodes.Status200OK,
                Ok = true,
                Data = new Dictionary<string, string>
                {
                    {
                        "token",
                        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InBscyIsIm5hbWVpZCI6IjEiLCJyb2xlIjoiYWRtaW4iLCJuYmYiOjE1ODA5MDI5MTMsImV4cCI6MTU4MDkxMDExMywiaWF0IjoxNTgwOTAyOTEzfQ.hv2hmW494K87URlgcBNTYSBZsAEc5QWXWWtnwDZa8UE"
                    }
                }
            };
        }
    }

    public class AuthenticateAccountNameNotFoundExample : IExamplesProvider<GenericHttpResponse>
    {
        public GenericHttpResponse GetExamples()
        {
            return new GenericHttpResponse
            {
                Code = StatusCodes.Status404NotFound,
                Ok = false,
                Data = new
                {
                    ErrorMessage = "AccountName [123] not found",
                }
            };
        }
    }
}