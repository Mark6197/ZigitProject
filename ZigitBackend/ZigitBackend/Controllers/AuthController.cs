using AutoMapper;
using DAL.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Json.Serialization;
using ZigitBackend.Authentication;
using ZigitBackend.DTOs;

namespace ZigitBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IMapper _mapper;

        public AuthController(IMapper mapper, IUserRepository userRepository, IJwtAuthManager jwtAuthManager)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtAuthManager = jwtAuthManager;
        }

        /// <summary>
        /// Login to the system
        /// </summary>
        /// <returns>Login Result</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /LoginRequest
        ///     {
        ///        "username": "admin@gmail.com",
        ///        "password": "Aa123456"
        ///     }
        /// </remarks>  
        /// <param name="request">Login request that holds the login credentials</param>
        /// <response code="200">Returns Successful Login Result</response>
        /// <response code="400">If one or more of the credential are empty</response> 
        /// <response code="401">If the user credentials are wrong</response> 
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResultDTO>> Login([FromBody] LoginRequest request)
        {
            long userId = _userRepository.TryLogin(request.Email, request.Password);//validate the login credentials
            if (userId == 0)
                return Unauthorized();

            var claims = new[]//generate claims array
            {
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            };

            PersonalDetails personalDetails = await _userRepository.GetUserPersonalDetails(userId);
            PersonalDetailsDTO personalDetailsDTO = _mapper.Map<PersonalDetailsDTO>(personalDetails);

            var jwtResult = _jwtAuthManager.GenerateTokens(request.Email, claims, DateTime.Now);//Invoke GenerateTokens and get JWTAuthResult
            return Ok(new LoginResultDTO(jwtResult.AccessToken,personalDetailsDTO));
        }

        /// <summary>
        /// Login request model
        /// </summary>
        public class LoginRequest
        {
            [Required]
            [JsonPropertyName("username")]
            public string Email { get; set; }

            [Required]
            [JsonPropertyName("password")]
            public string Password { get; set; }
        }
    }
}