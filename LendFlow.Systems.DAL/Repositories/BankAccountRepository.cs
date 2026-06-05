using Dapper;
using LendFlow.Systems.DAL.Interfaces;
using LendFlow.Systems.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LendFlow.Systems.DAL.Repositories
{
    public class BankAccountRepository(IConfiguration configuration) : IBankAccountRepository
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Database connection string is missing from configuration.");

        public async Task<bool> AddBankAccountAsync(int userId, AddBankAccountDTO accountDetails)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new
            {
                UserId = userId,
                AccountHolderName = accountDetails.AccountHolderName,
                BankAccountNumber = accountDetails.BankAccountNumber,
                BankRoutingCode = accountDetails.BankRoutingCode,
                BankName = accountDetails.BankName,
                IsPrimary = accountDetails.IsPrimary
            };

            // Executes the SP and automatically converts the scalar BIT return value to a C# bool
            return await connection.ExecuteScalarAsync<bool>(
                "sp_AddUserBankAccount",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<BankAccountResponseDTO>> GetAccountsByUserIdAsync(int userId)
        {
            using var connection = new SqlConnection(_connectionString);

            // Dapper completely replaces the while(reader.Read()) loop by directly 
            // mapping database column names to your DTO properties.
            return await connection.QueryAsync<BankAccountResponseDTO>(
                "sp_GetUserBankAccounts",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
