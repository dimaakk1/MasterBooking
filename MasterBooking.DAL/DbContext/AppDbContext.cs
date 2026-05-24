using MasterBooking.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.DAL.DbContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<BlockedSlot> BlockedSlots { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Client)
                .WithMany(u => u.ClientAppointments)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Master)
                .WithMany(u => u.MasterAppointments)
                .HasForeignKey(a => a.MasterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
