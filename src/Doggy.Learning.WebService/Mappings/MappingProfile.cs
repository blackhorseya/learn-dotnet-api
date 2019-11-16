using System.Collections.Generic;
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

            CreateMap<GroupRoleMap, string>().ConvertUsing(m => m.Role.Name);
        }
    }
}