using AutoMapper;
using LayeredArch.Core.Application.DTO.AuthDto;
using LayeredArch.Core.Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Application.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RefreshToken, RefreshTokenDto>();
        }
    }
}
