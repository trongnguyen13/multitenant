using Multitenant.Domain.Contracts;

namespace Multitenant.Domain.Entities
{
    public class Branch : BaseEntity, ITenant
    {
        public Branch(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string TenantId { get; set; }
    }
}
