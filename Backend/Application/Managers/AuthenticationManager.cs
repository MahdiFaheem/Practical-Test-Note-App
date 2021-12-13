using Application.DTOs.AuthenticationDTOs;
using Application.Interfaces.EncyptionInterfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Managers
{
    public interface IAuthenticationManager
    {
        Task<bool> RegisterUser(RegisterDTO registerDTO);
        Task<LoginResponseDTO> LoginUser(LoginRequestDTO loginRequestDTO);
    }

    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEncryptionService _encryptionService;

        public AuthenticationManager(
            IUserManager userManager, IMapper mapper,
            IAuthenticationService authenticationService,
            IEncryptionService encryptionService
            )
        {
            _userManager = userManager;
            _mapper = mapper;
            _authenticationService = authenticationService;
            _encryptionService = encryptionService;
        }

        #region Login

        public async Task<LoginResponseDTO> LoginUser(LoginRequestDTO loginRequestDTO)
        {
            var user = await _userManager.LoginUser(loginRequestDTO.Email);

            if (user == null || !_encryptionService.VerifyHash(user.Password, loginRequestDTO.Password))
            {
                return null;
            }

            LoginResponseDTO loginResponseDTO = new()
            {
                AccessToken = _authenticationService.CreateToken(user)
            };

            return loginResponseDTO;
        }

        #endregion

        #region Register

        public async Task<bool> RegisterUser(RegisterDTO registerDTO)
        {
            try
            {
                registerDTO.Password = _encryptionService.GenerateHash(registerDTO.Password);
                var user = _mapper.Map<User>(registerDTO);
                user.Id = Guid.NewGuid().ToString();

                await _userManager.RegisterUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
