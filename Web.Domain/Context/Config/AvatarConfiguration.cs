using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entities;
using System.Reflection.Emit;

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
