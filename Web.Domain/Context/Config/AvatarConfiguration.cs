using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Domain.Entities;

namespace Web.Domain.Context.Config
{
    public class AvatarConfiguration : IEntityTypeConfiguration<Avatar>
    {
        public void Configure(EntityTypeBuilder<Avatar> builder)
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
