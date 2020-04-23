using AutoMapper;
using LayeredArch.Api.Resources.Auth;
using LayeredArch.Core.Application.DTO.AuthDto;
using LayeredArch.Core.Application.DTO.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayeredArch.Api.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            // Map API resources to core layer DTO
            CreateMap<RegisterUserResource, RegisterUserDto>();

            CreateMap<CreateTokenResourcecs, RefreshTokenDto>()
                .ForMember(t => t.Id, opt => opt.MapFrom(tr => tr.RefreshToken));

            // Map core layer DTO to API resources
            CreateMap<RefreshTokenDto, TokenResource>()
                .ForMember(tr => tr.RefreshToken, opt => opt.MapFrom(t => t.Id));
        }
    }
}
