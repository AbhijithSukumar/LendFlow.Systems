using LendFlow.Systems.Models.Enums;

namespace LendFlow.Systems.Models.Entities
{
    public class InternalStaff
    {
        public int StaffId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public Department Department { get; set; }
        public bool IsActive { get; set; }
    }
}
