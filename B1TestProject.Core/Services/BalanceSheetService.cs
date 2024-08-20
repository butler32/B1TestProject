using B1TestProject.Core.DTO;
using B1TestProject.Core.Interfaces;
using B1TestProject.Domain.Entities;
using B1TestProject.Infrastructure.Interfaces;
using B1TestProject.Infrastructure.Repositories;

namespace B1TestProject.Core.Services
{
    public class BalanceSheetService : IBalanceSheetService
    {
        private readonly IExcelEntryRepository<BalanceSheetEntryEntity> _excelRepository;
        private readonly IDTOConverter _converter;

        public BalanceSheetService(IExcelEntryRepository<BalanceSheetEntryEntity> excelRepository, IDTOConverter dTOConverter)
        {
            _excelRepository = excelRepository;
            _converter = dTOConverter;
        }

        public async Task<BalanceSheetEntryEntity?> AddEntryAsync(BalanceSheetDTO balanceSheetDTO, CancellationToken cancellationToken)
        {
            return await _excelRepository.AddAsync(_converter.Convert(balanceSheetDTO), cancellationToken);
        }

        public async Task<bool> DeleteAllEntriesAsync(CancellationToken cancellationToken)
        {
            await _excelRepository.DeleteAllAsync(cancellationToken);
            return true;
        }

        public async Task<List<BalanceSheetDTO>> GetAllEntriesAsync(CancellationToken cancellationToken)
        {
            var result = await _excelRepository.GetAllAsync(cancellationToken);
            
            return result.Select(_converter.Convert).ToList();
        }
    }
}
