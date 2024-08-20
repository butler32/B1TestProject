using B1TestProject.Core.DTO;
using B1TestProject.Core.Interfaces;
using B1TestProject.Domain.Entities;

namespace B1TestProject.Core.Services
{
    public class DTOConverter : IDTOConverter
    {
        public TextLineDTO Convert(TextLineEntity entity)
        {
            return new TextLineDTO
            {
                Id = entity.Id,
                Date = entity.Date,
                Latin = entity.Latin,
                Russian = entity.Russian,
                IntegerNum = entity.IntegerNum,
                DoubleNum = entity.DoubleNum
            };
        }

        public TextLineEntity Convert(TextLineDTO dto)
        {
            return new TextLineEntity
            {
                Id = dto.Id,
                Date = dto.Date.ToUniversalTime(),
                Latin = dto.Latin,
                Russian = dto.Russian,
                IntegerNum = dto.IntegerNum,
                DoubleNum = dto.DoubleNum
            };
        }

        public BalanceSheetDTO Convert(BalanceSheetEntryEntity entity)
        {
            return new BalanceSheetDTO
            {
                Id = entity.Id,
                Class = entity.Class,
                AccountNumber = entity.AccountNumber,
                IncomingBalanceActive = entity.IncomingBalanceActive,
                IncomingBalancePassive = entity.IncomingBalancePassive,
                TurnoverDebit = entity.TurnoverDebit,
                TurnoverCredit = entity.TurnoverCredit,
                OutgoingBalanceActive = entity.IncomingBalanceActive + entity.TurnoverDebit - entity.TurnoverCredit,
                OutgoingBalancePassive = entity.IncomingBalancePassive + entity.TurnoverCredit - entity.TurnoverDebit
            };
        }

        public BalanceSheetEntryEntity Convert(BalanceSheetDTO dto)
        {
            return new BalanceSheetEntryEntity
            {
                Id = dto.Id,
                Class = dto.Class,
                AccountNumber = dto.AccountNumber,
                IncomingBalanceActive = dto.IncomingBalanceActive,
                IncomingBalancePassive = dto.IncomingBalancePassive,
                TurnoverDebit = dto.TurnoverDebit,
                TurnoverCredit = dto.TurnoverCredit
            };
        }

        public ExcelFilesDTO Convert(ExcelFilesEntity entity)
        {
            return new ExcelFilesDTO
            {
                Id = entity.Id,
                ExcelFileId = entity.ExcelFileId,
                Name = entity.Name,
                BalanceSheets = entity.BalanceSheetEntries.Select(Convert).ToList(),
            };
        }

        public ExcelFilesEntity Convert(ExcelFilesDTO dto)
        {
            return new ExcelFilesEntity
            {
                Id = dto.Id,
                ExcelFileId = dto.ExcelFileId,
                Name = dto.Name,
                BalanceSheetEntries = dto.BalanceSheets.Select(Convert).ToList()
            };
        }
    }
}
