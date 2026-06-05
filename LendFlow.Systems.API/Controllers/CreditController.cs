using LendFlow.Systems.BLL.Interfaces;
using LendFlow.Systems.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LendFlow.Systems.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditController(ICreditService creditService) : ControllerBase
    {
        private readonly ICreditService _creditService = creditService;

        /// <summary>
        /// Allows authenticated borrowers to submit a new loan request.
        /// </summary>
        [Authorize] // Requires any valid logged-in user token
        [HttpPost("apply")]
        public async Task<IActionResult> ApplyForLoan([FromBody] LoanApplicationRequestDTO request)
        {
            // Extract the User ID safely from the JWT NameIdentifier claim
            var userIdClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("User identifier is invalid or missing from token.");
            }

            int applicationId = await _creditService.SubmitLoanApplicationAsync(userId, request);

            return CreatedAtAction(nameof(ApplyForLoan), new { id = applicationId }, new { ApplicationId = applicationId, Message = "Application submitted successfully." });
        }

        /// <summary>
        /// Retrieves all applications with a status of 'Submitted' or 'UnderReview'.
        /// Restricted to Credit Underwriters.
        /// </summary>
        [Authorize(Roles = "CreditUnderwriter")]
        [HttpGet("applications/pending")]
        public async Task<IActionResult> GetPendingApplications()
        {
            var applications = await _creditService.GetPendingApplicationsAsync();
            return Ok(applications);
        }

        /// <summary>
        /// Processes a detailed risk analysis and logs an approval or rejection decision.
        /// Restricted to Credit Underwriters.
        /// </summary>
        [Authorize(Roles = "CreditUnderwriter")]
        [HttpPost("applications/{applicationId}/evaluate")]
        public async Task<IActionResult> EvaluateApplication(int applicationId, [FromBody] CreditEvaluationRequestDTO request)
        {
            // Extract the Internal Staff ID safely from the JWT NameIdentifier claim
            var staffIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(staffIdClaim) || !int.TryParse(staffIdClaim, out int underwriterStaffId))
            {
                return Unauthorized("Staff identifier is invalid or missing from token.");
            }

            bool isProcessed = await _creditService.ProcessLoanEvaluationAsync(underwriterStaffId, applicationId, request);

            if (!isProcessed)
            {
                return BadRequest("Evaluation failed. Verify that the application exists and has not already been finalized.");
            }

            return Ok(new { Message = $"Application {applicationId} has been successfully updated." });
        }
    }
}
