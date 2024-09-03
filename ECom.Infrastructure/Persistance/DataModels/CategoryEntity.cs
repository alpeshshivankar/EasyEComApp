using ECom.Domain;

namespace ECom.Infrastructure.Persistance.DataModels
{
    public class CategoryEntity : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

    }
}
