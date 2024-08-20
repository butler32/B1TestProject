using B1TestProject.Core.DTO;
using B1TestProject.Domain.Entities;

namespace B1TestProject.Core.Interfaces
{
    public interface IExcelDBService
    {
        Task<ExcelFilesEntity?> AddFileAsync(ExcelFilesDTO excelFileDTO, CancellationToken cancellationToken);
        Task<bool> DeleteAllFilesAsync(CancellationToken cancellationToken);
        Task<List<ExcelFilesDTO>> GetAllFilesAsync(CancellationToken cancellationToken);
    }
}
