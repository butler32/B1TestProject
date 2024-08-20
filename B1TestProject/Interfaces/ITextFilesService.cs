using B1TestProject.Core.DTO;

namespace B1TestProject.Interfaces
{
    public interface ITextFilesService
    {
        Task<bool> GenerateTextFiles(CancellationToken cancellationToken);
        Task ImportFileToDBAsync(List<string[]> lineData, CancellationToken cancellationToken);
        Task<int> MergeTextFiles(CancellationToken cancellationToken, string? filterString);
        bool IsMergedFileExist();
        bool IsTextFilesExist();
        Task DeleteAllFilesAsync(CancellationToken cancellationToken);
        Task<List<string>> SearchInFileAsync(string fileName, string searchQuery);
        int GetAmountOfLines();
    }
}