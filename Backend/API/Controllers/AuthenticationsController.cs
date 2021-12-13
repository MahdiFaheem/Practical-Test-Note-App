using Application.DTOs.AuthenticationDTOs;
using Application.DTOs.EntityDTOs;
using Application.DTOs.ResponseDTOs;
using Application.Extensions;
using Application.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AuthenticationsController : BaseController
    {
        private readonly IAuthenticationManager _authenticationManager;

        public AuthenticationsController(IAuthenticationManager authenticationManager, IApiResponseDTO apiResponseDTO)
        {
            _authenticationManager = authenticationManager;
            _apiResponseDTO = apiResponseDTO;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var result = await _authenticationManager.RegisterUser(registerDTO);

            return (result) ? NoContent() 
                : BadRequest(_apiResponseDTO.SetApiResponse("Registration failed"));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO loginRequestDTO)
        {
            var result = await _authenticationManager.LoginUser(loginRequestDTO);

            return (result != null) ? Ok(result) 
                : BadRequest(_apiResponseDTO.SetApiResponse("Email or Password does not match."));
        }
    }
}
