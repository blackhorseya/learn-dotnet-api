using System.Collections.Generic;
using System.Threading.Tasks;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Interfaces;
using Doggy.Learning.WebService.Models;
using Doggy.Learning.WebService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doggy.Learning.WebService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly RoleRepositoryBase _roleRepo;
        private readonly IUserService _userService;

        public UserController(RoleRepositoryBase roleRepo, IUserService userService)
        {
            _roleRepo = roleRepo;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserResponse>> Authenticate([FromBody] AuthenticateRequest request)
        {
            var user = await _userService.Authenticate(request.Username, request.Password);
            if (user == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            return Ok(new UserResponse
            {
                Name = user.Name,
                Roles = user.GetRolesName(),
                Token = user.Token,
            });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<Role>>> Get()
        {
            return await _roleRepo.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> Get(int id)
        {
            // todo: fix it
            if (!int.TryParse(User.Identity.Name, out var userId))
                return BadRequest();

            if (id != userId && !User.IsInRole("admin"))
                return Forbid();

            var group = await _userService.FindByIdAsync(id);

            return new UserResponse
            {
                Name = group.Name,
            };
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<UserResponse>> Post(UserRequest request)
        {
            var entity = new Group
            {
                Name = request.Name,
            };
            
            var result = await _userService.CreateUserAsync(entity);

            return new UserResponse
            {
                Name = result.Name,
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Role entity)
        {
            if (entity == null || id != entity.Id)
            {
                return BadRequest();
            }

            await _roleRepo.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Role>> Delete(int id)
        {
            var entity = await _roleRepo.DeleteAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }
    }
}