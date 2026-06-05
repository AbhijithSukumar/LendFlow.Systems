namespace LendFlow.Systems.Models.DTOs
{
    public record   RegistrationRequestDTO
    {
        // User Table
        public required string Email { get; init; }
        public required string Password { get; init; }

        // Personal Details Table
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public DateTime DateOfBirth { get; init; }
        public required string Phone { get; init; }
        public required string Address { get; init; }

        // Employment Details Table
        public required string EmployerName { get; init; }
        public required string JobTitle { get; init; }
        public decimal AnnualIncome { get; init; }
        public int EmploymentType { get; init; }

        // KYC Details Table
        public int DocumentType { get; init; }
        public required string DocumentNumber { get; init; }
    }
}