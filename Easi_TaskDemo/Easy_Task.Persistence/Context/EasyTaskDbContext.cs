using Easy_Task.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Easy_Task.Persistence.Context
{
    public class EasyTaskDbContext: IdentityDbContext<AppUser>
    {
        public EasyTaskDbContext(DbContextOptions<EasyTaskDbContext> options): base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure relationships and other constraints here if needed
            builder.Entity<Employee>()
                .HasOne(e => e.AppUser)
                .WithMany() 
                .HasForeignKey(e => e.AppUserId);
        }

    }
}
