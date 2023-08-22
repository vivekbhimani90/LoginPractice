using LoginPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginPractice.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<LocalUser> Users { get; set; } 

        public DbSet<PersonalDetails> UserDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalDetails>().HasData(
                new PersonalDetails
                {
                    Id = 1,
                    Name = "Vivek",
                    City = "Jamnagar",
                    Country = "India"
                },
               new PersonalDetails
               {
                   Id = 2,
                   Name = "Meet",
                   City = "Ahmedabad",
                   Country = "India"
               },
               new PersonalDetails
               {
                   Id = 3,
                   Name = "Dheeraj",
                   City = "Hydrabad",
                   Country = "India"
               }
                );
        }
    }
}
