using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doggy.LearnNetCore.Domain.Contexts;
using Doggy.LearnNetCore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Doggy.LearnNetCore.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly RbacContext _rbacContext;

        public ValuesController(RbacContext rbacContext)
        {
            _rbacContext = rbacContext;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post()
        {
            _rbacContext.Roles.Add(entity: new Role
            {
                Name = "test",
            });
            _rbacContext.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
