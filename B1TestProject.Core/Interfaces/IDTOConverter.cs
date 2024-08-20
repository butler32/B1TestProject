using B1TestProject.Core.DTO;
using B1TestProject.Domain.Entities;

namespace B1TestProject.Core.Interfaces
{
    public interface IDTOConverter
    {
        TextLineDTO Convert(TextLineEntity entity);
        TextLineEntity Convert(TextLineDTO dto);
        BalanceSheetDTO Convert(BalanceSheetEntryEntity entity);
        BalanceSheetEntryEntity Convert(BalanceSheetDTO dto);
        ExcelFilesDTO Convert(ExcelFilesEntity entity);
        ExcelFilesEntity Convert(ExcelFilesDTO dto);
    }
}
