using GMS.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the relationship between Member and Trainer
            modelBuilder.Entity<Member>()
                .HasOne(m => m.Trainer)
                .WithMany(t => t.Members)
                .HasForeignKey(m => m.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevents cascading delete

            // Configuring the relationship between Payment and Membership
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Membership)
                .WithMany(m => m.Payments)
                .HasForeignKey(p => p.MembershipId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevents cascading delete

            // Configuring the relationship between Payment and Package
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Package)
                .WithMany()
                .HasForeignKey(p => p.PackageId)
                .OnDelete(DeleteBehavior.NoAction);  // Prevents cascading delete

            // Configuring the relationship between Membership and Package
            modelBuilder.Entity<Membership>()
                .HasOne(m => m.Package)
                .WithMany()
                .HasForeignKey(m => m.PackageId)
                .OnDelete(DeleteBehavior.Restrict);  // Optional, depending on your requirements

            // Additional configurations for other relationships if needed
        }
    }
}
