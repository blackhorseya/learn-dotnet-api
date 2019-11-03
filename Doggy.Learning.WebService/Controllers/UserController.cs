using System.Collections.Generic;
using System.Threading.Tasks;
using Doggy.Learning.Auth.Data.Repositories;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Interfaces;
using Doggy.Learning.Auth.Domain.Models;
using Doggy.Learning.WebService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doggy.Learning.WebService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;
        private readonly IUserService _userService;

        public UserController(RoleRepository roleRepository, IUserService userService)
        {
            _roleRepository = roleRepository;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
        {
            var user = await _userService.Authenticate(request.Username, request.Password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<Role>>> Get()
        {
            return await _roleRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            if (!int.TryParse(User.Identity.Name, out var userId))
                return BadRequest();
            
            if (id != userId && !User.IsInRole("admin"))
                return Forbid();
            
            return await _userService.FindByIdAsync(id);
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