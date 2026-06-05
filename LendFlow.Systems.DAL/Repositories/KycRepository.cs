using Dapper;
using LendFlow.Systems.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LendFlow.Systems.DAL.Repositories
{
    public class KycRepository(IConfiguration configuration) : IKycRepository
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Database connection string 'DefaultConnection' is missing.");

        public async Task<bool> UpdateKYCWithAuditAsync(int userId, int status, int adminId, string notes)
        {
            using var connection = new SqlConnection(_connectionString);

            // Shifting from heavy DynamicParameters allocation to a clean inline anonymous object
            var parameters = new
            {
                UserId = userId,
                Status = status,
                AdminId = adminId,
                Remarks = notes // Maps your variable string down to the SP's @Remarks variable
            };

            // Using ExecuteScalarAsync to safely read the single outcome row count
            var result = await connection.ExecuteScalarAsync<int>(
                "sp_UpdateKYCWithAudit",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }

        public async Task<string?> GetKycStatusByUserIdAsync(int userId)
        {
            using var connection = new SqlConnection(_connectionString);

            // Executes the SP and reads the first scalar string value returned
            return await connection.QueryFirstOrDefaultAsync<string>(
                "sp_GetKycStatusByUserId",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure
            );
        }


    }
}