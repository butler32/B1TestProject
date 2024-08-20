namespace B1TestProject.Models
{
    public class BalanceSheetModel
    {
        public Guid Id { get; set; }
        public int Class { get; set; }
        public string AccountNumber { get; set; }
        public decimal IncomingBalanceActive { get; set; }
        public decimal IncomingBalancePassive { get; set; }
        public decimal TurnoverDebit { get; set; }
        public decimal TurnoverCredit { get; set; }
        public decimal OutgoingBalanceActive { get; set; }
        public decimal OutgoingBalancePassive { get; set; }
        public bool IsBold => AccountNumber.Length == 2 || IsSpecialRow;
        public bool IsClassHeader { get; set; }
        public bool IsSpecialRow { get; set; }
    }
}
