namespace LendFlow.Systems.Models.DTOs
{
    public record StaffLoginRequestDTO
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
