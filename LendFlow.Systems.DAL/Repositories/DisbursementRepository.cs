using Dapper;
using LendFlow.Systems.DAL.Interfaces;
using LendFlow.Systems.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text.Json;

namespace LendFlow.Systems.DAL.Repositories
{
    public class DisbursementRepository(IConfiguration configuration) : IDisbursementRepository
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Database connection string is missing.");

        public async Task<DisbursementResultDTO> ExecuteDisbursementAsync(int applicationId, int staffId, decimal amount, DisbursementRequestDTO details)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new
            {
                details.ApplicationId,
                StaffId = staffId,
                TransactionReference = details.TransactionReferenceNo
            };

            try
            {
                // Dapper maps parameters cleanly and executes scalar checks safely
                var isSuccess = await connection.ExecuteScalarAsync<bool>(
                    "sp_ExecuteDisbursement",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                if (isSuccess)
                {
                    return new DisbursementResultDTO
                    {
                        IsSuccess = true,
                        TransactionId = details.TransactionReferenceNo
                    };
                }

                return new DisbursementResultDTO
                {
                    IsSuccess = false,
                    ErrorMessage = "The database transaction failed to process or update the loan status."
                };
            }
            catch (Exception ex)
            {
                return new DisbursementResultDTO
                {
                    IsSuccess = false,
                    ErrorMessage = $"Database Exception: {ex.Message}"
                };
            }
        }

        public async Task<bool> SaveRepaymentScheduleAsync(int applicationId, IEnumerable<RepaymentScheduleDTO> scheduleRows)
        {
            // 1. Transform rows directly into an anonymous object array matching your JSON keys
            var mappedRows = scheduleRows.Select(row => new
            {
                row.InstallmentNumber,
                // Enforce ISO-8601 formatting for reliable time-zone parsing across SQL engines
                DueDate = row.DueDate.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                row.TotalInstallmentAmount,
                PaymentStatus = row.Status.Trim().Equals("pending", StringComparison.OrdinalIgnoreCase) ? 1 : 2
            });

            // 2. Convert the entire structured sequence into a clean single JSON collection string
            string jsonPayload = JsonSerializer.Serialize(mappedRows);

            using var connection = new SqlConnection(_connectionString);

            try
            {
                // 3. One atomic call across the wire. No explicit ADO.NET transactions needed!
                int rowsAffected = await connection.ExecuteAsync(
                    "sp_SaveRepaymentScheduleRow",
                    new
                    {
                        ApplicationId = applicationId,
                        ScheduleJson = jsonPayload
                    },
                    commandType: CommandType.StoredProcedure
                );

                return true;
            }
            catch
            {
                // Any parsing failure or schema issue instantly fails the entire SP batch automatically
                return false;
            }
        }

        public async Task<(decimal ApprovedLimit, int TenureMonths)?> GetApprovedLoanDetailsAsync(int applicationId)
        {
            using var connection = new SqlConnection(_connectionString);

            // Querying structural data profiles effortlessly into anonymous types
            var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                "sp_GetApprovedLoanDetails",
                new { ApplicationId = applicationId },
                commandType: CommandType.StoredProcedure
            );

            if (result != null)
            {
                return (
                    ApprovedLimit: (decimal)result.ApprovedLimit,
                    TenureMonths: (int)result.TenureMonths
                );
            }

            return null;
        }

        public async Task<IEnumerable<ApprovedLoanQueueDTO>> GetApprovedLoansQueueAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            // Dapper automatically matches database record set names directly to target property targets
            return await connection.QueryAsync<ApprovedLoanQueueDTO>(
                "sp_GetApprovedLoansQueue",
                commandType: CommandType.StoredProcedure
            );
        }
    }
}