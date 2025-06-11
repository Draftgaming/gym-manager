using DataAccess.Models;

using Microsoft.EntityFrameworkCore;

using System;

namespace DataAccess
{
    public class GymContext(DbContextOptions<GymContext> options) : DbContext(options)
    {
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        public DbSet<ProgressRecord> ProgressRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgressRecord>().Property(t => t.ProgressRecordId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Trainer>().Property(t => t.TrainerId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Trainer>()
                .HasMany(t => t.Trainees)
                .WithOne(tr => tr.Trainer)
                .HasForeignKey(tr => tr.TrainerId);

            modelBuilder.Entity<TrainingPlan>().Property(t => t.TrainingPlanId).ValueGeneratedOnAdd();
            modelBuilder.Entity<TrainingPlan>()
                .HasMany(p => p.Trainees)
                .WithOne(t => t.TrainingPlan)
                .HasForeignKey(t => t.TrainingPlanId);

            modelBuilder.Entity<Trainee>().Property(t => t.TraineeId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Trainee>()
                .HasMany(t => t.ProgressRecords)
                .WithOne(p => p.Trainee)
                .HasForeignKey(p => p.TraineeId);

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Trainers
            modelBuilder.Entity<Trainer>().HasData(
                new Trainer { TrainerId = -1, Name = "John Doe", Specialty = "Strength Training" },
                new Trainer { TrainerId = -2, Name = "Jane Smith", Specialty = "Cardio & Endurance" });

            // Seed Training Plans
            modelBuilder.Entity<TrainingPlan>().HasData(
                new TrainingPlan { TrainingPlanId = -1, PlanName = "Beginner Strength", Description = "Basic strength exercises" },
                new TrainingPlan { TrainingPlanId = -2, PlanName = "Cardio Blast", Description = "High-intensity cardio workouts" });

            // Seed Trainees (now 5 total)
            modelBuilder.Entity<Trainee>().HasData(
                new Trainee
                {
                    TraineeId = -1,
                    FullName = "Alex Johnson",
                    DateJoined = DateTime.UtcNow.AddMonths(-2),
                    TrainerId = -1,
                    TrainingPlanId = -1,
                    Role = "User",
                    Password = "password123",
                    Username = "alexjohnson"
                },
                new Trainee
                {
                    TraineeId = -2,
                    FullName = "Maria Garcia",
                    DateJoined = DateTime.UtcNow.AddMonths(-1),
                    TrainerId = -2,
                    TrainingPlanId = -2,
                    Role = "User",
                    Password = "password123",
                    Username = "mariagarcia"
                },
                new Trainee
                {
                    TraineeId = -3,
                    FullName = "David Lee",
                    DateJoined = DateTime.UtcNow.AddMonths(-3),
                    TrainerId = -1,
                    TrainingPlanId = -1,
                    Role = "User",
                    Password = "password123",
                    Username = "davidlee"
                },
                new Trainee
                {
                    TraineeId = -4,
                    FullName = "Emily Clark",
                    DateJoined = DateTime.UtcNow.AddMonths(-1).AddDays(-10),
                    TrainerId = -2,
                    TrainingPlanId = -2,
                    Role = "User",
                    Password = "password123",
                    Username = "emilyclark"
                },
                new Trainee
                {
                    TraineeId = -5,
                    FullName = "Samuel Brown",
                    DateJoined = DateTime.UtcNow.AddMonths(-2).AddDays(-15),
                    TrainerId = -1,
                    TrainingPlanId = -1,
                    Role = "User",
                    Password = "password123",
                    Username = "samuelbrown"
                },
                new Trainee
                {
                    TraineeId = -6,
                    FullName = "Admin Admin",
                    DateJoined = DateTime.UtcNow.AddMonths(-2).AddDays(-15),
                    TrainerId = -1,
                    TrainingPlanId = -1,
                    Role = "Administrator",
                    Password = "password123",
                    Username = "admin"
                });

            // Seed Progress Records (example records)
            modelBuilder.Entity<ProgressRecord>().HasData(
                new ProgressRecord
                {
                    ProgressRecordId = -1,
                    RecordDate = DateTime.UtcNow.AddDays(-14),
                    Notes = "Initial assessment",
                    Weight = 75.0,
                    Bmi = 24.5,
                    TraineeId = -1
                },
                new ProgressRecord
                {
                    ProgressRecordId = -2,
                    RecordDate = DateTime.UtcNow.AddDays(-7),
                    Notes = "First-week progress",
                    Weight = 74.0,
                    Bmi = 24.2,
                    TraineeId = -1
                },
                new ProgressRecord
                {
                    ProgressRecordId = -3,
                    RecordDate = DateTime.UtcNow.AddDays(-10),
                    Notes = "Initial assessment",
                    Weight = 68.0,
                    Bmi = 22.0,
                    TraineeId = -2
                });
        }
    }
}
