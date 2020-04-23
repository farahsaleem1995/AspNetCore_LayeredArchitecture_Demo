using AutoMapper;
using LayeredArch.Core.Application.DTO.AuthDto;
using LayeredArch.Core.Domain.Models.Auth;
using LayeredArch.Core.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Application.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            // Map core layer DTO to domain models
            CreateMap<RegisterUserDto, DomainUser>();

            // Map domain models to core layer DTO
            CreateMap<RefreshToken, RefreshTokenDto>();
        }
    }
}
