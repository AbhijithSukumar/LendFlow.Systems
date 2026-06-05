using LendFlow.Systems.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LendFlow.Systems.BLL.Interfaces
{
    public interface IBankAccountService
    {
       
        Task<bool> AddBankAccountAsync(int userId, AddBankAccountDTO accountDetails);

        Task<IEnumerable<BankAccountResponseDTO>> GetAccountsByUserIdAsync(int userId);
    }
}