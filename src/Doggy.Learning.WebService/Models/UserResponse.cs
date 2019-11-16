using System;
using System.Collections.Generic;

namespace Doggy.Learning.WebService.Models
{
    public class UserResponse
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string> Roles { get; set; }
    }
}