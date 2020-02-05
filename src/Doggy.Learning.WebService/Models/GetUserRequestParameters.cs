using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Doggy.Learning.WebService.Models
{
    public class GetUserRequestParameters : RequestParametersBase
    {
        [Required]
        [FromRoute(Name = "name")]
        public string Name { get; set; }
    }
}