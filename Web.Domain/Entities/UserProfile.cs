using Web.Model.EnumerationTypes;

namespace Web.Domain.Entities
{
    public partial class UserProfile : AuditEntity<Guid>
    {
        public UserProfile()
        {
            Users = new HashSet<User>();
        }
        public string Name { get; set; }
        public ProfileType Type { get; set; }
        public string? Descrtiption { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
