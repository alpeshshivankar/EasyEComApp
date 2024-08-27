using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECom.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

    }
}
