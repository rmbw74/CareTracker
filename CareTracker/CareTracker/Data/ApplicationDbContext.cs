using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CareTracker.Models;

namespace CareTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Dependent> Dependent { get; set; }
        public DbSet<DependentDoctor> DependentDoctor { get; set; }
        public DbSet<DependentUser> DependentUser { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<SharedDependent> SharedDependent { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
