using LendFlow.Systems.BLL.Interfaces;
using LendFlow.Systems.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LendFlow.Systems.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO request)
        {
            if (request == null) return BadRequest("Invalid request data.");

            // The BLL handles the transaction and mapping
            bool isSuccess = await _userService.RegisterFullUserProfileAsync(request);

            if (isSuccess)
            {
                return Ok(new { message = "Registration completed successfully!" });
            }

            return StatusCode(500, "A database error occurred during registration.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            // The UserService now handles fetching the user, verifying the password, 
            // and generating the token in one go.
            var token = await _userService.LoginUserAsync(request);

            if (token == null)
            {
                // return Unauthorized if the email is wrong OR the password doesn't match
                return Unauthorized(new { message = "Invalid Email or Password." });
            }

            // If we have a token, the user is authenticated and the token 
            // contains their current KycStatus (Pending, Verified, etc.)
            return Ok(new
            {
                Token = token,
                Message = "Login Successful"
            });
        }
    }
}
