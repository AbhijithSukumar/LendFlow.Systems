using LendFlow.Systems.Models.Entities;

using System.Data;

namespace LendFlow.Systems.DAL.Interfaces;

public interface IUserRepository
{
    Task<int> RegisterUserAsync(User user, IDbTransaction transaction);
    Task<bool> AddPersonalDetailsAsync(PersonalDetail personal, IDbTransaction transaction);
    Task<bool> AddEmploymentDetailsAsync(EmploymentDetail employment, IDbTransaction transaction);
    Task<bool> AddKYCDetailsAsync(KYCDetail kyc, IDbTransaction transaction);
    Task<User?> GetUserByEmailAsync(string email);
}
