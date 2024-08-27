using System.ComponentModel.DataAnnotations;

namespace ECom.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
