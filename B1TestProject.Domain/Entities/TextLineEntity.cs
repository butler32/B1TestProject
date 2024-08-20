namespace B1TestProject.Domain.Entities
{
    public class TextLineEntity : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Latin { get; set; }
        public string Russian { get; set; }
        public int IntegerNum { get; set; }
        public double DoubleNum { get; set; }
    }
}
