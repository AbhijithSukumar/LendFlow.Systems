namespace LendFlow.Systems.Models.DTOs
{
    public record LoginRequestDTO
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
