using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Domain.Test.UserProfile;

namespace Web.Domain.Test.Context.Config
{
    public class UserProfileEntityConfiguration : IEntityTypeConfiguration<UserProfileEntity>
    {
        public void Configure(EntityTypeBuilder<UserProfileEntity> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .HasMany(e => e.Users)
                .WithOne(e => e.UserProfile)
                .HasForeignKey(e => e.UserProfileId)
                .IsRequired(false);
        }
    }
}
