using Microsoft.AspNetCore.Mvc;

namespace Doggy.Learning.WebService.Models
{
    public class RequestParametersBase
    {
        [FromHeader]
        public string ApplicationName { get; set; }
    }
}