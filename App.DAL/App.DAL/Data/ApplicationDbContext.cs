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
        public virtual DbSet<DailyTransaction> DailyTransactions { get; set; }
        public virtual DbSet<TransactionItemCategory> TransactionItemCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Delete Behavior
            modelBuilder.Entity<Organization>() // user can't delete organization if it contain emails
                    .HasMany(X => X.Emails)
                    .WithOne(e => e.Organization)
                    .HasForeignKey(e => e.OrganizationId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>()
                .HasMany(x => x.Owners)
                .WithOne(c => c.Company)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict); 
            #endregion

            #region Unique
            modelBuilder.Entity<Company>()
                .HasIndex(x => x.TaxRegistrationNumber)
                .IsUnique();

            modelBuilder.Entity<Owner>()
                .HasIndex(x => new { x.NationalId, x.CompanyId })
                .IsUnique();

            modelBuilder.Entity<Email>()
                .HasIndex(x=> new {x.EmailAddress,x.CompanyId,x.OrganizationId})
                .IsUnique();

            modelBuilder.Entity<Selector>()
                .HasIndex(x => new { x.Guid })
                .IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(x=> new { x.Email })
                .IsUnique();

            modelBuilder.Entity<DailyTransaction>()
                .HasIndex(x => x.TransactionNumber)
                .IsUnique();
            #endregion

            #region Sequence
            modelBuilder.HasSequence<long>("TransactionNumberSequence")
                    .StartsAt(1)
                    .IncrementsBy(1);

            modelBuilder.Entity<DailyTransaction>()
                .Property(t => t.TransactionNumber)
                .HasDefaultValueSql("NEXT VALUE FOR TransactionNumberSequence"); 
            #endregion

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser()
            {
                Id = 1,
                Name = "Sherif Dahy",
                Email = "admin",
                Password = "G2Po4Wgp2rqN2Aflcd61PwfgSPy8v0D37XXNFFZzhWk=",
                UserType = UserType.Admin
            });
            base.OnModelCreating(modelBuilder);
        }

        
    }
}
