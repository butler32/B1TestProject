using B1TestProject.Core.DTO;
using B1TestProject.Core.Interfaces;
using B1TestProject.Domain.Entities;
using B1TestProject.Infrastructure.Interfaces;

namespace B1TestProject.Core.Services
{
    public class TextLinesService : ITextLinesService
    {
        private readonly ITextRepository<TextLineEntity> _textRepository;
        private readonly IDTOConverter _converter;

        public TextLinesService(ITextRepository<TextLineEntity> textRepository, IDTOConverter dTOConverter)
        {
            _textRepository = textRepository;
            _converter = dTOConverter;
        }

        public async Task AddBunchAsync(List<TextLineDTO> textLineDTOs, CancellationToken cancellationToken)
        {
            await _textRepository.InsertButchAsync(textLineDTOs.Select(_converter.Convert).ToList(), cancellationToken);
        }

        public async Task<TextLineEntity?> AddLineAsync(TextLineDTO textLine, CancellationToken cancellationToken)
        {
            return await _textRepository.AddAsync(_converter.Convert(textLine), cancellationToken);
        }

        public async Task<bool> DeleteAllLinesAsync(CancellationToken cancellationToken)
        {
            await _textRepository.DeleteAllAsync(cancellationToken);
            return true;
        }
    }
}
