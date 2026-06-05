using LendFlow.Systems.BLL.Interfaces;
using LendFlow.Systems.BLL.Services;
using LendFlow.Systems.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LendFlow.Systems.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KycController(IKycService kycService) : ControllerBase
    {
        [Authorize(Roles = "KycOfficer")]
        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] KYCUpdateRequestDTO request)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            var result = await kycService.ProcessKYCVerificationAsync(adminId, request);

            return result ? Ok("KYC processed and logged.") : BadRequest("Update failed.");
        }

        [HttpGet("my-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMyKycStatus()
        {
            // Extract the borrower's logged-in user ID from token
            var userIdClaim = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "User identity context is missing or invalid." });
            }

            var status = await kycService.CheckVerificationStatus(userId);

            if (string.IsNullOrEmpty(status))
            {
                return NotFound(new { message = "No KYC record found associated with this account identifier." });
            }

            // Returns a clean anonymous payload containing your status text/code
            return Ok(new { kycStatus = status });
        }
    }

}
