using B1TestProject.Core.DTO;
using B1TestProject.Domain.Entities;

namespace B1TestProject.Core.Interfaces
{
    public interface ITextLinesService
    {
        Task<TextLineEntity?> AddLineAsync(TextLineDTO textLine, CancellationToken cancellationToken);
        Task<bool> DeleteAllLinesAsync(CancellationToken cancellationToken);
        Task AddBunchAsync(List<TextLineDTO> textLineDTOs, CancellationToken cancellationToken);
    }
}
