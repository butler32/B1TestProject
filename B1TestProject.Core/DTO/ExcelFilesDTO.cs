namespace B1TestProject.Core.DTO
{
    public class ExcelFilesDTO
    {
        public Guid Id { get; set; }
        public Guid ExcelFileId { get; set; }
        public string Name { get; set; }
        public ICollection<BalanceSheetDTO> BalanceSheets { get; set; }
    }
}
