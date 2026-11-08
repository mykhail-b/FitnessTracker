
using FitnessTracker.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> UserProfiles => Set<AppUser>();
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<WorkoutExercise> Exercises => Set<WorkoutExercise>();
    public DbSet<WorkoutSet> WorkoutSets => Set<WorkoutSet>();
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet<MealItem> MealItems => Set<MealItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // IdentityUser -> AppUser (1:1)
        modelBuilder.Entity<AppUser>()
            .HasOne<IdentityUser>()
            .WithOne()
            .HasForeignKey<AppUser>(u => u.IdentityId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // AppUser -> Workout (1:N)
        modelBuilder.Entity<Workout>()
            .HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(w => w.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // AppUser -> Meal (1:N)
        modelBuilder.Entity<Meal>()
            .HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Workout -> WorkoutSet (1:N)
        modelBuilder.Entity<Workout>()
            .HasMany(w => w.WorkoutSets)
            .WithOne()
            .HasForeignKey(ws => ws.WorkoutId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // WorkoutExercise -> WorkoutSet (1:N)
        modelBuilder.Entity<WorkoutSet>()
            .HasOne<WorkoutExercise>()
            .WithMany()
            .HasForeignKey(ws => ws.ExerciseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
