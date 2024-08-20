namespace B1TestProject.Domain.Entities
{
    public class ExcelFilesEntity : BaseEntity
    {
        public Guid ExcelFileId { get; set; }
        public string Name { get; set; }

        public ICollection<BalanceSheetEntryEntity> BalanceSheetEntries { get; set; }
    }
}
