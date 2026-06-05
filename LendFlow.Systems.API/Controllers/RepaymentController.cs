using LendFlow.Systems.BLL.Interfaces;
using LendFlow.Systems.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LendFlow.Systems.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class RepaymentController(IRepaymentService repaymentService) : ControllerBase
    {
        private readonly IRepaymentService _repaymentService = repaymentService;

        /// <summary>
        /// Processes a pre-determined payment amount to settle either the current active EMI or force close the loan.
        /// </summary>
        [HttpPost("submit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SubmitPayment([FromBody] ProcessRepaymentDTO request)
        {
            try
            {
                bool isProcessed = await _repaymentService.ProcessRepaymentAsync(request);

                if (!isProcessed)
                {
                    return BadRequest(new { message = "Payment transaction could not be processed at this time." });
                }

                return Ok(new { message = "Payment recorded and balances updated successfully." });
            }
            catch (ArgumentException ex)
            {
                // Captures custom validation messages from the BLL/Stored Procedure 
                // (e.g., if the UI sent an outdated amount that doesn't match the current database state)
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while processing the ledger transaction." });
            }
        }

        /// <summary>
        /// Fetches top-level card metrics for the logged-in borrower's dashboard.
        /// </summary>
        [HttpGet("my-summary")]
        [ProducesResponseType(typeof(BorrowerLoanSummaryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMySummary()
        {
            // Extract the UserId from the logged-in user's JWT token
            var userIdClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "User identity context is missing or invalid." });
            }

            var summary = await _repaymentService.GetBorrowerSummaryAsync(userId);
            if (summary == null)
            {
                return NotFound(new { message = "No active loan applications or schedules found for this account." });
            }

            return Ok(summary);
        }

        /// <summary>
        /// Returns the complete tabular repayment schedule grid for a specific loan.
        /// </summary>
        [HttpGet("schedule/{applicationId}")]
        [ProducesResponseType(typeof(IEnumerable<LoanScheduleGridItemDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetLoanSchedule(int applicationId)
        {
            // Note: In a production app, you'd verify if the logged-in user owns this applicationId,
            // but for learning/testing, fetching the grid directly is perfect!
            var grid = await _repaymentService.GetScheduleGridAsync(applicationId);
            return Ok(grid);
        }

        /// <summary>
        /// Returns a master ledger overview of all current loans in the system for administrative monitoring.
        /// </summary>
        [HttpGet("loans/active")]
        [Authorize(Roles = "FinanceOfficer")] // ◄ Blocks regular customers from accessing global metrics
        [ProducesResponseType(typeof(IEnumerable<AdminActiveLoanOverviewDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetActivePortfolio()
        {
            var portfolio = await _repaymentService.GetAdminActiveLoansAsync();
            return Ok(portfolio);
        }
    }
}
