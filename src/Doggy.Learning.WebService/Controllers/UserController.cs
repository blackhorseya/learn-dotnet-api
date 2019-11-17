using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Filters;
using Doggy.Learning.Auth.Domain.Interfaces;
using Doggy.Learning.Infrastructure.Constants;
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
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserResponse>> Authenticate([FromBody] AuthenticateRequest request)
        {
            var token = await _userService.Authenticate(request.Username, request.Password);
            if (string.IsNullOrEmpty(token))
                return BadRequest(new {message = "Username or password is incorrect"});

            return Ok(new Dictionary<string, string>
            {
                {"token", token}
            });
        }

        [HttpGet]
        [Rbac(ModuleConstants.Management)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> Get()
        {
            var groups = await _userService.FindAllAsync();
            var res = _mapper.Map<List<UserResponse>>(groups);

            return Ok(res);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<UserResponse>> Get(string name)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out var userId))
                return BadRequest();

            if (name != User.Identity.Name && !User.IsInRole("admin"))
                return Forbid();

            var group = await _userService.FindByIdAsync(userId);
            var res = _mapper.Map<UserResponse>(group);

            return Ok(res);
        }
    }
}