using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Domain.Test.Avatar;

namespace Web.Domain.Test.Context.Config
{
    public class AvatarEntityConfiguration : IEntityTypeConfiguration<AvatarEntity>
    {
        public void Configure(EntityTypeBuilder<AvatarEntity> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .HasMany(e => e.Users)
                .WithOne(e => e.Avatar)
                .HasForeignKey(e => e.AvatarId)
                .IsRequired(false);
        }
    }
}
