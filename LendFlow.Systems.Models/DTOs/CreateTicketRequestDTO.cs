namespace LendFlow.Systems.Models.DTOs
{
    public record CreateTicketRequestDTO
    {
        public required string Subject { get; init; }
        public required string Description { get; init; }
        public int Priority { get; init; } = 1;
    }
}
