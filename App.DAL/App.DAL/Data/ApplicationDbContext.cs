using App.Entities.Enums;
using App.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser()
            {
                Id = 1,
                Name = "Sherif Dahy",
                Email = "admin",
                Password = "333Sherif%",
                UserType = UserType.Admin
                
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
