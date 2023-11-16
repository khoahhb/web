using Web.Model.Enum;

namespace Web.Domain.Entities
{
    public partial class User : AuditEntity<Guid>
    {
        public User()
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
        public virtual Avatar? Avatar { get; set; }
        public Guid UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

    }
}
