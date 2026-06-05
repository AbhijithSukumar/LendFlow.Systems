using Dapper;
using LendFlow.Systems.DAL.Interfaces;
using LendFlow.Systems.Models.Entities;
using LendFlow.Systems.Models.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LendFlow.Systems.DAL.Repositories
{
    public class CreditRepository(IConfiguration configuration) : ICreditRepository
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Database connection string 'DefaultConnection' is missing.");

        public async Task<int> CreateApplicationAsync(LoanApplication application)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new
            {
                UserId = application.UserId,
                AmountRequested = application.AmountRequested,
                TenureMonths = application.TenureMonths,
                ApplicationDate = application.ApplicationDate,
                ApplicationStatus = application.ApplicationStatus
            };

            // Dapper returns the identity ID scalar value cast directly into an int
            return await connection.ExecuteScalarAsync<int>(
                "sp_CreateLoanApplication",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<LoanApplication?> GetApplicationByIdAsync(int applicationId)
        {
            using var connection = new SqlConnection(_connectionString);

            // If your SP returns positional columns instead of named columns, 
            // Dapper handles the mapping smoothly if names match, otherwise it handles entity schemas nicely.
            return await connection.QueryFirstOrDefaultAsync<LoanApplication>(
                "sp_GetLoanApplicationById",
                new { ApplicationId = applicationId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<LoanApplication>> GetPendingApplicationsAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            // Replaces the entire while loop and manual list generation in one clean shot
            return await connection.QueryAsync<LoanApplication>(
                "sp_GetPendingLoanApplications",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> UpdateApplicationStatusAsync(int applicationId, int applicationStatus, decimal approvedLimit, DateTime updatedDate)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new
            {
                ApplicationId = applicationId,
                ApplicationStatus = applicationStatus,
                ApprovedLimit = approvedLimit,
                UpdatedDate = updatedDate
            };

            // Evaluates the internal execution outcome scalar directly into a boolean check
            var rowsAffected = await connection.ExecuteScalarAsync<int>(
                "sp_UpdateLoanApplicationStatus",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected > 0;
        }

        public async Task<bool> LogCreditEvaluationAsync(CreditEvaluation evaluation)
        {
            using var connection = new SqlConnection(_connectionString);

            // Dapper handles null values inside anonymous types out of the box—no more (object?) or DBNull.Value checks!
            var parameters = new
            {
                ApplicationId = evaluation.ApplicationId,
                UnderwriterStaffId = evaluation.UnderwriterStaffId,
                CreditScore = evaluation.CreditScore,
                DebtToIncomeRatio = evaluation.DebtToIncomeRatio,
                ApprovalLimit = evaluation.ApprovalLimit,
                UnderwriterNotes = evaluation.UnderwriterNotes,
                EvaluationDate = evaluation.EvaluationDate
            };

            var rowsAffected = await connection.ExecuteScalarAsync<int>(
                "sp_LogCreditEvaluation",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected > 0;
        }

        public async Task<bool> IsUserKycVerifiedAsync(int userId)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.ExecuteScalarAsync<bool>(
                "sp_CheckUserKycStatus",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}