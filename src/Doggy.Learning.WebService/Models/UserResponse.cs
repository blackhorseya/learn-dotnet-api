using System;
using System.Collections.Generic;
using Doggy.Extensions.Http.Response;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Doggy.Learning.WebService.Models
{
    public class UserResponse
    {
        public string Name { get; set; }
        // public DateTime CreatedAt { get; set; }
        // public DateTime UpdatedAt { get; set; }
        public List<RoleResponse> Roles { get; set; }
    }

    public class GetUserByNameSuccessResponseExample : IExamplesProvider<GenericHttpResponse>
    {
        public GenericHttpResponse GetExamples()
        {
            return new GenericHttpResponse
            {
                Code = StatusCodes.Status200OK,
                Ok = true,
                Data = new UserResponse
                {
                    Name = "wfbss",
                    Roles = new List<RoleResponse>
                    {
                        new RoleResponse
                        {
                            Name = "wfbss_manager",
                            Modules = new List<string> {"key"},
                        },
                        new RoleResponse
                        {
                            Name = "wfbss_director",
                            Modules = new List<string> {"key"},
                        }
                    }
                }
            };
        }
    }
}