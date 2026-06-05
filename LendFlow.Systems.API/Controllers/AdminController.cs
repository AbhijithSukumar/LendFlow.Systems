using LendFlow.Systems.BLL.Interfaces;
using LendFlow.Systems.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LendFlow.Systems.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController(IAdminService adminService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] StaffRegisterRequestDTO request)
        {
            var id = await adminService.RegisterAdminAsync(request);
            return Ok(new { StaffId = id, Message = "Admin created successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] StaffLoginRequestDTO request)
        {
            var token = await adminService.LoginAsync(request);
            if (token == null) return Unauthorized("Invalid Admin Credentials.");
            return Ok(new { Token = token });
        }


    }
}
