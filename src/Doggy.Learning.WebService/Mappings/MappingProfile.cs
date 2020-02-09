using AutoMapper;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.WebService.Models;

namespace Doggy.Learning.WebService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRequest, Group>();
            CreateMap<Group, UserResponse>()
                .ForMember(dst => dst.Roles, opt => opt.Condition(src => src.Roles != null && src.Roles.Count != 0));

            CreateMap<RoleServiceMap, string>().ConvertUsing(x => x.Service.Name);
            CreateMap<RoleModuleMap, string>().ConvertUsing(x => x.Module.Name);
            CreateMap<GroupRoleMap, RoleResponse>()
                .ForMember(r => r.Name, opt => opt.MapFrom(x => x.Role.Name))
                .ForMember(r => r.Services, opt => opt.Condition(x => x.Role.Services != null && x.Role.Services.Count != 0))
                .ForMember(r => r.Services, opt => opt.MapFrom(x => x.Role.Services))
                .ForMember(r => r.Modules, opt => opt.Condition(x => x.Role.Modules != null && x.Role.Modules.Count != 0))
                .ForMember(r => r.Modules, opt => opt.MapFrom(x => x.Role.Modules));
        }
    }
}