using Web.Domain.Test.Base;
using Web.Model.Enum;
using Web.Domain.Test.Avatar;
using Web.Domain.Test.UserProfile;

namespace Web.Domain.Test.User
{
    public partial class UserEntity : AuditEntity<Guid>
    {
        public UserEntity()
        {
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public GenderType? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public Guid? AvatarId { get; set; }
        public virtual AvatarEntity? Avatar { get; set; }
        public Guid UserProfileId { get; set; }
        public virtual UserProfileEntity UserProfile { get; set; }

    }
}
