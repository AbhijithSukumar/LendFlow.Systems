using LendFlow.Systems.BLL.Interfaces;
using LendFlow.Systems.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace LendFlow.Systems.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BankAccountController(IBankAccountService bankAccountService) : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService = bankAccountService;

        /// <summary>
        /// Registers a new bank account profile for the authenticated user.
        /// </summary>
        /// <param name="request">The bank transit, tracking, and routing specifications.</param>
        /// <returns>An HTTP 200 OK status on successful persistence, or an HTTP 400 Bad Request error.</returns>
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddBankAccount([FromBody] AddBankAccountDTO request)
        {
            // 1. EXTRACT USING YOUR EXACT JWT CLAIM KEY: Change ClaimTypes.NameIdentifier to "id"
            var userIdClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                // Pro-tip for your local debugging: If you are testing via Swagger/Postman 
                // and haven't passed the Bearer token yet, uncomment the line below to bypass the block:
                // userIdClaim = "1"; 

                return Unauthorized(new { message = "User identity is missing or invalid." });
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest(new { message = "Invalid user identification format." });
            }

            // 2. Delegate the data execution to the BLL Service Layer
            bool isSaved = await _bankAccountService.AddBankAccountAsync(userId, request);

            if (!isSaved)
            {
                return BadRequest(new { message = "Failed to add bank account. The account may already exist or data validation failed." });
            }

            return Ok(new { message = "Bank account registered successfully." });
        }
    }
}
