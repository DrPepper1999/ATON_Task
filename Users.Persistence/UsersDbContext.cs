using Microsoft.EntityFrameworkCore;
using Users.Application.Interfaces;
using Users.Domain;
using Users.Persistence.EntityTypeConfigurations;

namespace Users.Persistence
{
    public class UsersDbContext : DbContext, IUserDbContext
    {
        public DbSet<User> Users { get; set; }
        public UsersDbContext(DbContextOptions<UsersDbContext> options) 
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
