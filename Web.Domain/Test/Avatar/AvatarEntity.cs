using Web.Domain.Test.Base;
using Web.Domain.Test.User;

namespace Web.Domain.Test.Avatar
{
    public partial class AvatarEntity : AuditEntity<Guid>
    {
        public AvatarEntity()
        {
            Users = new HashSet<UserEntity>();
        }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public long FileSize { get; set; }
        public bool IsPublished { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
