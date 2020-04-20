using AutoMapper;
using LayeredArch.Core.Application.Resources.AuthDto;
using LayeredArch.Core.Application.Resources.IdentityDto;
using LayeredArch.Core.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayeredArch.Core.Application.Mapping
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            // Map domain model to services DTO
            CreateMap<DomainUser, UserDto>()
                .ForMember(au => au.Roles, opt => opt.MapFrom(du => 
                    du.UserRoles.Select(ur => new RoleDto()
                    {
                        Id = ur.Role.Id,
                        Name = ur.Role.Name
                    })));

            CreateMap<DomainRole, RoleDto>();

            // Map services DTO domain model
            CreateMap<RegisterUserDto, DomainUser>();

            CreateMap<UserDto, DomainUser>();

            CreateMap<RoleDto, DomainRole>();
        }
    }
}
