
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
    public DbSet<Food> Foods => Set<Food>();

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

        modelBuilder.Entity<Food>(entity =>
        {
            entity.Property(food => food.Name).HasMaxLength(100).IsRequired();
            entity.Property(food => food.CaloriesPer100g).HasPrecision(18, 4);
            entity.Property(food => food.ProteinPer100g).HasPrecision(18, 4);
            entity.Property(food => food.FatPer100g).HasPrecision(18, 4);
            entity.Property(food => food.CarbsPer100g).HasPrecision(18, 4);
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.Property(meal => meal.Name).HasMaxLength(100).IsRequired();
            entity.HasMany(meal => meal.Items)
                .WithOne()
                .HasForeignKey(item => item.MealId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MealItem>(entity =>
        {
            entity.Property(item => item.Grams).HasPrecision(18, 2);
            entity.HasOne(item => item.Food)
                .WithMany()
                .HasForeignKey(item => item.FoodId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
