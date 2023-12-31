﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Domain.Entities;

namespace Web.Domain.Context.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .HasMany(u => u.Credentials)
                .WithOne(cre => cre.User)
                .HasForeignKey(cre => cre.UserId)
                .IsRequired(false);
        }
    }
}
