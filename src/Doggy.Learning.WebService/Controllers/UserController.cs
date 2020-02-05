using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Doggy.Learning.Auth.Domain.Filters;
using Doggy.Learning.Auth.Domain.Interfaces;
using Doggy.Learning.Infrastructure.Constants;
using Doggy.Learning.WebService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Dictionary<string, string>))]
        public async Task<IActionResult> Authenticate([FromQuery] RequestParametersBase param,
            [FromBody] AuthenticateRequestBody requestBody)
        {
            var token = await _userService.Authenticate(requestBody.Username, requestBody.Password);
            if (string.IsNullOrEmpty(token))
                return BadRequest(new {message = "Username or password is incorrect"});

            // todo: refactor return type
            return new ObjectResult(new Dictionary<string, string>
            {
                {"token", token}
            });
        }

        [HttpGet]
        [Rbac(ModuleConstants.Management)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> Get([FromQuery] RequestParametersBase param)
        {
            var groups = await _userService.FindAllAsync();
            var res = _mapper.Map<List<UserResponse>>(groups);

            return Ok(res);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<UserResponse>> Get([FromQuery] GetUserRequestParameters param)
        {
            if (param.Name != User.Identity.Name && !User.IsInRole("admin"))
                return Forbid();

            var group = await _userService.FindByNameAsync(param.Name);
            var res = _mapper.Map<UserResponse>(group);

            return Ok(res);
        }
    }
}