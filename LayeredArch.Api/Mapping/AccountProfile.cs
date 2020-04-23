using AutoMapper;
using LayeredArch.Api.Resources.Account;
using LayeredArch.Core.Application.DTO.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayeredArch.Api.Mapping
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            // Map core layer DTO to API resources
            CreateMap<UserDto, UserReducedResource>()
            .ForMember(ur => ur.UserRoles,
              opt => opt.MapFrom(uDto => uDto.Roles.Select(ur => new RoleResource()
              {
                  Id = ur.Id,
                  Name = ur.Name
              })))
            .IncludeAllDerived();

            CreateMap<UserDto, UserResource>()
              .ForMember(ur => ur.PhoneNumber, opt => opt.MapFrom(uDto => uDto.PhoneNumberConfirmed ? uDto.PhoneNumber : null))
              .ForMember(ur => ur.Email, opt => opt.MapFrom(uDto => uDto.EmailConfirmed ? uDto.Email : null))
              .IncludeAllDerived();

            CreateMap<UserDto, UserAdminstrationResource>();

            CreateMap<UserDto, RoleResource>();

            // Map API resources to core layer DTO
            CreateMap<UserQueryResource, UserQueryDto>();

            CreateMap<UpdateUserResource, UpdateUserDto>();
        }
    }
}
