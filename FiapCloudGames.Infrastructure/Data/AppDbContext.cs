using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Enums;
using FiapCloudGames.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Game> Games => Set<Game>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica todos os mappings do assembly automaticamente
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Users.AnyAsync(u => u.Email == new Email("admin@fcg.com")))
                return;

            User.ValidateRawPassword("Admin@123");
            var passwordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123");

            var admin = new User(
                name: "Administrador",
                email: "admin@fcg.com",
                passwordHash: passwordHash,
                role: UserRole.Admin
            );

            await context.Users.AddAsync(admin);
            await context.SaveChangesAsync();
        }
    }
}
