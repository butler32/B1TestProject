using B1TestProject.Core.DTO;

namespace B1TestProject.Interfaces
{
    public interface IExcelFilesService
    {
        Task ImportExcelToDBAsync(string filePath, CancellationToken cancellationToken);
        Task<List<ExcelFilesDTO>> GetAllFilesFromDBAsync(CancellationToken cancellationToken);
        Task DeleteAllFilesFromDBAsync(CancellationToken cancellationToken);
    }
}
