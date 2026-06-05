using LendFlow.Systems.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LendFlow.Systems.DAL.Interfaces
{
    public interface IBankAccountRepository
    {
        /// <summary>
        /// Inserts a new bank profile under the user's relational identity.
        /// </summary>
        Task<bool> AddBankAccountAsync(int userId, AddBankAccountDTO accountDetails);

        /// <summary>
        /// Fetches all active banking configurations linked to the user.
        /// </summary>
        Task<IEnumerable<BankAccountResponseDTO>> GetAccountsByUserIdAsync(int userId);
    }
}
