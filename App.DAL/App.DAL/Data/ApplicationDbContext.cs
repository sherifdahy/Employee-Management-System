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
        public virtual DbSet<Selector> Selectors { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            
            modelBuilder.Entity<Organization>() // user can't delete organization if it contain emails
                .HasMany(X=>X.Emails)
                .WithOne(e=>e.Organization)
                .HasForeignKey(e=>e.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);
                

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser()
            {
                Id = Guid.NewGuid(),
                Name = "Sherif Dahy",
                Email = "admin",
                Password = "G2Po4Wgp2rqN2Aflcd61PwfgSPy8v0D37XXNFFZzhWk=",
                UserType = UserType.Admin
            });
            base.OnModelCreating(modelBuilder);
        }

        
    }
}
