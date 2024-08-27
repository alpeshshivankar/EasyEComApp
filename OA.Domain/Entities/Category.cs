using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ECom.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string CategoryName { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
       
    }
}
