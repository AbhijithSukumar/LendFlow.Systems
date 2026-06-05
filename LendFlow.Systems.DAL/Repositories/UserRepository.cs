using Dapper;
using LendFlow.Systems.DAL.Interfaces;
using LendFlow.Systems.Models.Entities;
using LendFlow.Systems.Models.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LendFlow.Systems.DAL.Repositories
{
    public class UserRepository(IConfiguration configuration) : IUserRepository
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Database connection string 'DefaultConnection' is missing from configuration.");

        public async Task<int> RegisterUserAsync(User user, IDbTransaction transaction)
        {
            if (transaction.Connection == null)
                throw new InvalidOperationException("The transaction does not have an active connection.");

            // Standardized anonymous parameters replace verbose DynamicParameters blocks
            var parameters = new
            {
                Email = user.Email,
                PasswordHash = user.PasswordHash
            };

            // Using ExecuteScalarAsync to safely pull back the newly generated UserId key
            return await transaction.Connection.ExecuteScalarAsync<int>(
                "sp_RegisterUser",
                parameters,
                transaction: transaction,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> AddPersonalDetailsAsync(PersonalDetail personal, IDbTransaction transaction)
        {
            if (transaction.Connection == null)
                throw new InvalidOperationException("The transaction does not have an active connection.");

            var parameters = new
            {
                UserId = personal.UserId,
                FirstName = personal.FirstName,
                LastName = personal.LastName,
                DateOfBirth = personal.DateOfBirth,
                Phone = personal.Phone,
                Address = personal.Address
            };

            var result = await transaction.Connection.ExecuteAsync(
                "sp_AddPersonalDetails",
                parameters,
                transaction: transaction,
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }

        public async Task<bool> AddEmploymentDetailsAsync(EmploymentDetail employment, IDbTransaction transaction)
        {
            if (transaction.Connection == null)
                throw new InvalidOperationException("The transaction does not have an active connection.");

            var parameters = new
            {
                UserId = employment.UserId,
                EmployerName = employment.EmployerName,
                JobTitle = employment.JobTitle,
                AnnualIncome = employment.AnnualIncome,
                EmploymentType = (int)employment.EmploymentType
            };

            var result = await transaction.Connection.ExecuteAsync(
                "sp_AddEmploymentDetails",
                parameters,
                transaction: transaction,
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }

        public async Task<bool> AddKYCDetailsAsync(KYCDetail kyc, IDbTransaction transaction)
        {
            if (transaction.Connection == null)
                throw new InvalidOperationException("The transaction does not have an active connection.");

            var parameters = new
            {
                UserId = kyc.UserId,
                DocumentType = (int)kyc.DocumentType,
                DocumentNumber = kyc.DocumentNumber,
                VerificationStatus = (int)kyc.VerificationStatus
            };

            var result = await transaction.Connection.ExecuteAsync(
                "sp_AddKYCDetails",
                parameters,
                transaction: transaction,
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using var connection = new SqlConnection(_connectionString);

            // Keeping your multi-mapping logic perfectly intact while optimizing execution performance
            var userList = await connection.QueryAsync<User, KYCDetail, User>(
                "sp_GetUserByEmail",
                (user, kyc) =>
                {
                    // Fallback to avoid nullable reference exceptions if KYC isn't started yet
                    user.KYCInfo = kyc ?? new KYCDetail { VerificationStatus = (VerificationStatus)1 };
                    return user;
                },
                new { Email = email },
                splitOn: "UserId",
                commandType: CommandType.StoredProcedure
            );

            return userList.FirstOrDefault();
        }
    }
}