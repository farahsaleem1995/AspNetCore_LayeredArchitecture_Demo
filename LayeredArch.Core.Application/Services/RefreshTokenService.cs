using AutoMapper;
using LayeredArch.Core.Application.DTO.AuthDto;
using LayeredArch.Core.Application.DTO.IdentityDto;
using LayeredArch.Core.Application.Exceptions;
using LayeredArch.Core.Application.Interfaces;
using LayeredArch.Core.Domain.Interfaces;
using LayeredArch.Core.Domain.Models.Auth;
using LayeredArch.Core.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Core.Application.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly SignInManager<DomainUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository,
                                    SignInManager<DomainUser> signInManager,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RefreshTokenDto> GetNew(string userId, string accessToken)
        {
            await _refreshTokenRepository.RemoveByUserIdAsync(userId);

            var refreshToken = new RefreshToken() { UserId = userId, AccessToken = accessToken };
            _refreshTokenRepository.Add(refreshToken);
            await _unitOfWork.CompleteAsync();

            var refreshTokenDto = _mapper.Map<RefreshToken, RefreshTokenDto>(refreshToken);
            return refreshTokenDto;
        }

        public async Task<UserDto> CheckUser(RefreshTokenDto token)
        {
            var refreshToken = await _refreshTokenRepository.Get(token.Id, token.AccessToken);
            if (refreshToken == null || !await _signInManager.CanSignInAsync(refreshToken.User))
            {
                throw new CoreException(401, "Invalid request");
            }

            var userDto = _mapper.Map<DomainUser, UserDto>(refreshToken.User);
            return (userDto);
        }
    }
}
