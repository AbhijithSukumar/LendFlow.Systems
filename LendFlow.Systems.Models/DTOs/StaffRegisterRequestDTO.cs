namespace LendFlow.Systems.Models.DTOs
{
    public record StaffRegisterRequestDTO
    {
        public required string FullName { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
        public required int DepartmentId { get; init; }


    }
}
