using System.Collections.Generic;
using System.Threading.Tasks;
using Doggy.Learning.Auth.Data.Repositories;
using Doggy.Learning.Auth.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Doggy.Learning.WebService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;

        public AuthController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> Get()
        {
            return await _roleRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> Get(int id)
        {
            return await _roleRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Role>> Post(Role entity)
        {
            return await _roleRepository.AddAsync(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Role entity)
        {
            if (entity == null || id != entity.Id)
            {
                return BadRequest();
            }

            await _roleRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Role>> Delete(int id)
        {
            var entity = await _roleRepository.DeleteAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }
    }
}