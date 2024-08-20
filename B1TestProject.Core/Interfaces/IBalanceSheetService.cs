using B1TestProject.Core.DTO;
using B1TestProject.Domain.Entities;

namespace B1TestProject.Core.Interfaces
{
    public interface IBalanceSheetService
    {
        Task<BalanceSheetEntryEntity?> AddEntryAsync(BalanceSheetDTO balanceSheetDTO, CancellationToken cancellationToken);
        Task<bool> DeleteAllEntriesAsync(CancellationToken cancellationToken);
        Task<List<BalanceSheetDTO>> GetAllEntriesAsync(CancellationToken cancellationToken);
    }
}
