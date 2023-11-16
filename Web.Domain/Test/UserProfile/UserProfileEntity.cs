using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Test.Base;
using Web.Domain.Test.User;
using Web.Model.Enum;

namespace Web.Domain.Test.UserProfile
{
    public partial class UserProfileEntity : AuditEntity<Guid>
    {
        public UserProfileEntity()
        {
            Users = new HashSet<UserEntity>();
        }
        public string Name { get; set; }
        public ProfileType Type { get; set; }
        public string? Descrtiption { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
