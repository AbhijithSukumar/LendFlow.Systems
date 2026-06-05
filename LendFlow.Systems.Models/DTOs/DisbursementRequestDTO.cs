using System.ComponentModel.DataAnnotations;

namespace LendFlow.Systems.Models.DTOs
{
    public record DisbursementRequestDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A valid application identifier must be provided.")]
        public int ApplicationId { get; init; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "A valid payment gateway reference string is required.")]
        public string TransactionReferenceNo { get; init; } = null!;
    }
}