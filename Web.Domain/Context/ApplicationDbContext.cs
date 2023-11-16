using Microsoft.EntityFrameworkCore;
using Web.Domain.Context.Config;
using Web.Domain.Entities;

namespace Web.Domain.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<Avatar> Avatars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Add configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
            //modelBuilder.ApplyConfiguration(new AvatarConfiguration());

            //Seed data 
            new DataSeeder(modelBuilder).Seed();
        }

        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
