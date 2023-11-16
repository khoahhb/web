using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entities;

namespace Web.Domain.Context.Config
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
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
