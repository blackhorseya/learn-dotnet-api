using System.Linq;
using System.Security.Claims;
using Doggy.Learning.Auth.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Doggy.Learning.Auth.Domain.Filters
{
    public class RbacAttribute : TypeFilterAttribute
    {
        public RbacAttribute(string module) : base(typeof(RbacFilter))
        {
            Arguments = new object[] {module};
        }
    }

    public class RbacFilter : IAuthorizationFilter
    {
        private readonly string _module;
        private readonly RoleRepositoryBase _roleRepo;

        public RbacFilter(string module, RoleRepositoryBase roleRepo)
        {
            _module = module;
            _roleRepo = roleRepo;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;

            if (isAuthenticated && context.HttpContext.User.Identity is ClaimsIdentity currentUser)
            {
                var isValid = false;
                var roles = currentUser.FindAll(ClaimTypes.Role);
                foreach (var roleName in roles)
                {
                    var role = _roleRepo.FindByNameAsync(roleName.Value).Result;
                    if (role != null && role.Modules.Exists(map => map.Module.Name == _module))
                        isValid = true;

                    if (isValid)
                        break;
                }

                if (!isValid)
                    context.Result = new ForbidResult();
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}