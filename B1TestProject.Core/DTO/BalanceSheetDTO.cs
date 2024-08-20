namespace B1TestProject.Core.DTO
{
    public class BalanceSheetDTO
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
    }
}
