using Microsoft.EntityFrameworkCore;
using Web.Domain.Test.Avatar;
using Web.Domain.Test.Context.Config;
using Web.Domain.Test.User;
using Web.Domain.Test.UserProfile;

namespace Web.Domain.Test.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserProfileEntity> Profiles { get; set; }
        public DbSet<AvatarEntity> Avatars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Add configurations
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserProfileEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AvatarEntityConfiguration());

            //Seed data 
            new DataSeederTest(modelBuilder).Seed();
        }

        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
