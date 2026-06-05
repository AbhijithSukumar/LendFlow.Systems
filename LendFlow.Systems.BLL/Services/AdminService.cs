using LendFlow.Systems.BLL.Interfaces;
using LendFlow.Systems.DAL.Interfaces;
using LendFlow.Systems.Models.DTOs;
using LendFlow.Systems.Models.Entities;
using LendFlow.Systems.Models.Enums;
using Microsoft.Extensions.Logging;

namespace LendFlow.Systems.BLL.Services
{
    public class AdminService(
        IStaffRepository staffRepo,
        TokenService tokenService,
        ILogger<AdminService> logger) : IAdminService
    {
        public async Task<int> RegisterAdminAsync(StaffRegisterRequestDTO staff)
        {
            logger.LogInformation("Attempting to register a new admin staff member with Email: {Email}", staff.Email);

            try
            {
                var staffEntity = new InternalStaff
                {
                    FullName = staff.FullName,
                    Email = staff.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(staff.Password),
                    Department = (Department)staff.DepartmentId,
                    IsActive = true
                };

                int newStaffId = await staffRepo.RegisterStaffAsync(staffEntity);

                logger.LogInformation("Successfully registered staff member. Assigned StaffId: {StaffId}", newStaffId);
                return newStaffId;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during staff registration for Email: {Email}", staff.Email);
                throw;
            }
        }

        public async Task<string?> LoginAsync(StaffLoginRequestDTO loginDetails)
        {
            logger.LogInformation("Processing login attempt for Email: {Email}", loginDetails.Email);

            try
            {
                var staff = await staffRepo.GetStaffByEmailAsync(loginDetails.Email);

                if (staff == null || !BCrypt.Net.BCrypt.Verify(loginDetails.Password, staff.PasswordHash))
                {
                    logger.LogWarning("Failed login attempt for Email: {Email} (Invalid credentials)", loginDetails.Email);
                    return null;
                }

                logger.LogInformation("Successful authentication for Staff ID: {StaffId}", staff.StaffId);

                return tokenService.GenerateTokenForStaff(staff);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An exception occurred during the login process for Email: {Email}", loginDetails.Email);
                throw;
            }
        }

    }
}
