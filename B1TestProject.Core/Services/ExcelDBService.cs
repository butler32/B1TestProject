using B1TestProject.Core.DTO;
using B1TestProject.Core.Interfaces;
using B1TestProject.Domain.Entities;
using B1TestProject.Infrastructure.Interfaces;

namespace B1TestProject.Core.Services
{
    public class ExcelDBService : IExcelDBService
    {
        private readonly IExcelFileRepository<ExcelFilesEntity> _repository;
        private readonly IDTOConverter _converter;

        public ExcelDBService(IExcelFileRepository<ExcelFilesEntity> repository, IDTOConverter dTOConverter)
        {
            _repository = repository;
            _converter = dTOConverter;
        }

        public async Task<ExcelFilesEntity?> AddFileAsync(ExcelFilesDTO excelFileDTO, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(_converter.Convert(excelFileDTO), cancellationToken);
        }

        public async Task<bool> DeleteAllFilesAsync(CancellationToken cancellationToken)
        {
            await _repository.DeleteAllAsync(cancellationToken);
            return true;
        }

        public async Task<List<ExcelFilesDTO>> GetAllFilesAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(cancellationToken);
            var list = new List<ExcelFilesDTO>();

            foreach(var file in result)
            {
                list.Add(_converter.Convert(file));
            }


            return result.Select(_converter.Convert).ToList();
        }
    }
}
