using LendFlow.Systems.BLL.Interfaces;
using LendFlow.Systems.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LendFlow.Systems.WebAPI.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "FinanceOfficer,Admin")] // Restrict to authorized internal staff roles
    public class DisbursementController(IDisbursementService disbursementService) : ControllerBase
    {
        private readonly IDisbursementService _disbursementService = disbursementService;

        /// <summary>
        /// Executes a bank payout for an approved loan and generates its amortization repayment schedule.
        /// </summary>
        /// <param name="applicationId">The target loan application ID passed in the route path.</param>
        /// <param name="request">The transactional payload containing the selected BankAccountId and TransactionReferenceNo.</param>
        /// <returns>A structured result indicating success or error details.</returns>
        [HttpPost("execute/{applicationId:int}")]
        [Authorize(Roles = "FinanceOfficer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DisbursementResultDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ExecuteDisbursement(int applicationId, [FromBody] DisbursementRequestDTO request)
        {
            // 1. Extract the staff identity processing this payout from token claims
            // For early development testing, you can mock this: int staffId = 101;
            var staffIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(staffIdClaim))
            {
                return Unauthorized(new { message = "Staff authentication context is missing or invalid." });
            }

            if (!int.TryParse(staffIdClaim, out int staffId))
            {
                return BadRequest(new { message = "Invalid staff identification format." });
            }

            // 2. Delegate the validation, banking ledger insert, and schedule calculations to the BLL
            var result = await _disbursementService.ProcessDisbursementAsync(staffId, applicationId, request);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Fetches the queue of all approved loan applications awaiting fund disbursement.
        /// </summary>
        /// <returns>A collection of approved loans ready for payout processing.</returns>
        [HttpGet("queue")]
        [Authorize(Roles = "FinanceOfficer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ApprovedLoanQueueDTO>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetApprovedLoansQueue()
        {
            var queue = await _disbursementService.GetApprovedLoansQueueAsync();
            return Ok(queue);
        }
    }
}
