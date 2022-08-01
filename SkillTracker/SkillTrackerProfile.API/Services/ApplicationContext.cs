using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Cosmos;
using SkillTrackerProfile.API.Models;

namespace SkillTrackerProfile.API.Services
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
                : base(options)
        {
        }  
        public DbSet<ProfileEntity> Profile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileEntity>(p =>
            {
                p.ToContainer("SkillTrackerContainer");
                p.HasKey(x => x.EmpId);
                //p.HasPartitionKey(x => x.skills.Select(s=>s.Name));
                p.OwnsMany(s => s.skills);
            });
        }

    }
}