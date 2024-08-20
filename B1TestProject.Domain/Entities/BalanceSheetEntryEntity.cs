namespace B1TestProject.Domain.Entities
{
    public class BalanceSheetEntryEntity : BaseEntity
    {
        public int Class { get; set; }
        public string AccountNumber { get; set; }
        public decimal IncomingBalanceActive { get; set; }
        public decimal IncomingBalancePassive { get; set; }
        public decimal TurnoverDebit { get; set; }
        public decimal TurnoverCredit { get; set; }

        public Guid ExcelFilesId { get; set; }
        public ExcelFilesEntity ExcelFiles { get; set; }
    }
}
