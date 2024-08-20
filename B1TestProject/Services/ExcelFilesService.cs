using B1TestProject.Core.DTO;
using B1TestProject.Core.Interfaces;
using B1TestProject.Interfaces;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Windows;

namespace B1TestProject.Services
{
    public class ExcelFilesService : IExcelFilesService
    {
        private readonly IExcelDBService _excelDBService;

        public async Task ImportExcelToDBAsync(string filePath, CancellationToken cancellationToken)
        {
            try
            {
                ExcelFilesDTO excelFilesDTO = new ExcelFilesDTO();
                excelFilesDTO.Id = Guid.NewGuid();
                excelFilesDTO.ExcelFileId = Guid.NewGuid();
                excelFilesDTO.Name = filePath.Split("\\").Last();
                excelFilesDTO.BalanceSheets = new List<BalanceSheetDTO>();

                int classCounter = 0;

                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var workbook = new HSSFWorkbook(fs);
                    var sheet = workbook.GetSheetAt(0);

                    for (int row = 8; row < sheet.LastRowNum; row++)
                    {
                        var dataRow = sheet.GetRow(row);
                        if (dataRow == null)
                        {
                            continue;
                        }

                        if (dataRow.GetCell(0).ToString()!.Split(' ')[0] == "КЛАСС")
                        {
                            classCounter++;
                            continue;
                        }

                        string accountNumber = dataRow.GetCell(0).ToString()!;
                        decimal incomingBalanceActive = decimal.Parse(dataRow.GetCell(1).ToString()!);
                        decimal incomingBalancePassive = decimal.Parse(dataRow.GetCell(2).ToString()!);
                        decimal turnoverDebit = decimal.Parse(dataRow.GetCell(3).ToString()!);
                        decimal turnoverCredit = decimal.Parse(dataRow.GetCell(4).ToString()!);

                        var entry = new BalanceSheetDTO
                        {
                            Id = Guid.NewGuid(),
                            Class = classCounter,
                            AccountNumber = accountNumber,
                            IncomingBalanceActive = incomingBalanceActive,
                            IncomingBalancePassive = incomingBalancePassive,
                            TurnoverDebit = turnoverDebit,
                            TurnoverCredit = turnoverCredit
                        };

                        excelFilesDTO.BalanceSheets.Add(entry);
                    }

                    await _excelDBService.AddFileAsync(excelFilesDTO, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task<List<ExcelFilesDTO>> GetAllFilesFromDBAsync(CancellationToken cancellationToken)
        {
            return await _excelDBService.GetAllFilesAsync(cancellationToken);
        }

        public async Task DeleteAllFilesFromDBAsync(CancellationToken cancellationToken)
        {
            await _excelDBService.DeleteAllFilesAsync(cancellationToken);
        }

        public ExcelFilesService(IExcelDBService excelDBService)
        {
            _excelDBService = excelDBService;
        }
    }
}
