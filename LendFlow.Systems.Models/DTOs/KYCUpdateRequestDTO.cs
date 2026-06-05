namespace LendFlow.Systems.Models.DTOs
{
    public record KYCUpdateRequestDTO
    (
        int UserId,
        int NewStatus, // e.g., 2 for Verified, 3 for Rejected
        string AdminNotes
    );
}
