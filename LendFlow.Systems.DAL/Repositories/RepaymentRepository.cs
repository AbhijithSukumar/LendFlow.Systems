using Dapper;
using LendFlow.Systems.DAL.Interfaces;
using LendFlow.Systems.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LendFlow.Systems.DAL.Repositories
{
    public class RepaymentRepository(IConfiguration configuration) : IRepaymentRepository
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Database connection string is missing from configuration.");

        public async Task<bool> SubmitRepaymentAsync(ProcessRepaymentDTO paymentDetails)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new
            {
                paymentDetails.ApplicationId,
                paymentDetails.PaymentAmount,
                paymentDetails.PaymentMethod,
                paymentDetails.TransactionReferenceNo
            };

            // Dapper executes the SP and casts the bit scalar response directly to a C# bool
            return await connection.ExecuteScalarAsync<bool>(
                "sp_ProcessRepayment",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<BorrowerLoanSummaryDTO?> GetBorrowerSummaryAsync(int userId)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<BorrowerLoanSummaryDTO>(
                "sp_GetBorrowerSummary",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<LoanScheduleGridItemDTO>> GetScheduleGridAsync(int applicationId)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<LoanScheduleGridItemDTO>(
                "sp_GetLoanScheduleGrid",
                new { ApplicationId = applicationId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<AdminActiveLoanOverviewDTO>> GetAdminActiveLoansAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<AdminActiveLoanOverviewDTO>(
                "sp_GetAdminActiveLoans",
                commandType: CommandType.StoredProcedure
            );
        }
    }
}