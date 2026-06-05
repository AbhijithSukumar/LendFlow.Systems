using Dapper;
using LendFlow.Systems.DAL.Interfaces;
using LendFlow.Systems.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LendFlow.Systems.DAL.Repositories
{
    public class StaffRepository(IConfiguration configuration) : IStaffRepository
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Database connection string 'DefaultConnection' is missing from configuration.");

        public async Task<int> RegisterStaffAsync(InternalStaff staff)
        {
            using var connection = new SqlConnection(_connectionString);

            // Shifting from verbose DynamicParameters to a crisp anonymous type model configuration
            var parameters = new
            {
                FullName = staff.FullName,
                Email = staff.Email,
                PasswordHash = staff.PasswordHash,
                Department = (int)staff.Department // Dapper natively handles enum mapping from explicit underlying value types
            };

            // Using ExecuteScalarAsync to pull back the newly inserted identity row key
            return await connection.ExecuteScalarAsync<int>(
                "sp_RegisterStaff",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<InternalStaff?> GetStaffByEmailAsync(string email)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<InternalStaff>(
                "sp_GetStaffByEmail",
                new { Email = email },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}