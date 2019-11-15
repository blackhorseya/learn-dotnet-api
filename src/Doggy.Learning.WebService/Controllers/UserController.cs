using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                Id = user.Id,
                Name = user.Name,
                Roles = user.GetRolesName(),
                Token = user.Token,
            });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> Get()
        {
            var users = await _userService.FindAllAsync();
            var results = users.Select(u => new UserResponse
            {
                Id = u.Id,
                Name = u.Name,
                Roles = u.GetRolesName(),
            }).ToList();

            return results;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> Get(int id)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out var userid))
                return BadRequest();
                
            if (id != userid && !User.IsInRole("admin"))
                return Forbid();

            var user = await _userService.FindByIdAsync(id);

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Roles = user.GetRolesName(),
            };
        }
    }
}