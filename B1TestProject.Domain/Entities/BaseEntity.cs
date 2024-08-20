using System.ComponentModel.DataAnnotations;

namespace B1TestProject.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
