using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Web.Model.Enum;

namespace Web.Domain.Entities
{
    public class User : BaseEnitity
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        public GenderType Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public Guid? AvatarId { get; set; }
        public Avatar? Avatar { get; set; }

        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; } = null!;
    }
}
