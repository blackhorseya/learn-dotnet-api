using System.Collections.Generic;
using System.Linq;
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
            CreateMap<Group, UserResponse>();

            CreateMap<RoleServiceMap, string>().ConvertUsing(x => x.Service.Name);
            CreateMap<RoleModuleMap, string>().ConvertUsing(x => x.Module.Name);
            CreateMap<GroupRoleMap, RoleResponse>()
                .ForMember(r => r.Name, opt => opt.MapFrom(x => x.Role.Name))
                .ForMember(r => r.Services, opt => opt.MapFrom(x => x.Role.Services))
                .ForMember(r => r.Modules, opt => opt.MapFrom(x => x.Role.Modules));
        }
    }
}