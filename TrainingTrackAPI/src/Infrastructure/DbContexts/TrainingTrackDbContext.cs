using Microsoft.EntityFrameworkCore;
using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Infrastructure.DbContexts;

public partial class TrainingTrackDbContext : DbContext
{
    public TrainingTrackDbContext()
    {
    }

    public TrainingTrackDbContext(DbContextOptions<TrainingTrackDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DietPlan> DietPlans { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<MealFood> MealFoods { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFood> UserFoods { get; set; }

    public virtual DbSet<UserMealFood> UserMealFoods { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=TrainingTrackDB;Trusted_Connection=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DietPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DietPlan");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Calories)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("calories");
            entity.Property(e => e.Carbs)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("carbs");
            entity.Property(e => e.Fat)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("fat");
            entity.Property(e => e.IsDisabled).HasColumnName("isDisabled");
            entity.Property(e => e.PlanName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("planName");
            entity.Property(e => e.Protein)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("protein");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.DietPlans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DietPlans_Users");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaloriesPer100g)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("caloriesPer100g");
            entity.Property(e => e.CarbsPer100g)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("carbsPer100g");
            entity.Property(e => e.FatPer100g)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("fatPer100g");
            entity.Property(e => e.FoodName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("foodName");
            entity.Property(e => e.ProteinPer100g)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("proteinPer100g");
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DietPlanId).HasColumnName("dietPlanId");
            entity.Property(e => e.MealName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("mealName");
            entity.Property(e => e.TimeOfEating)
                .HasPrecision(0)
                .HasColumnName("timeOfEating");

            entity.HasOne(d => d.DietPlan).WithMany(p => p.Meals)
                .HasForeignKey(d => d.DietPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Meals_DietPlans");
        });

        modelBuilder.Entity<MealFood>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FoodId).HasColumnName("foodId");
            entity.Property(e => e.MealId).HasColumnName("mealId");
            entity.Property(e => e.QuantityInGrams)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("quantityInGrams");

            entity.HasOne(d => d.Food).WithMany(p => p.MealFoods)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MealFoods_Foods");

            entity.HasOne(d => d.Meal).WithMany(p => p.MealFoods)
                .HasForeignKey(d => d.MealId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MealFoods_Meals");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.ProfilePicture).HasColumnName("profilePicture");
            entity.Property(e => e.UserMail)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("userMail");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<UserFood>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaloriesPer100g)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("caloriesPer100g");
            entity.Property(e => e.CarbsPer100g)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("carbsPer100g");
            entity.Property(e => e.FatPer100g)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("fatPer100g");
            entity.Property(e => e.FoodName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("foodName");
            entity.Property(e => e.ProteinPer100g)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("proteinPer100g");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.UserFoods)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserFoods_Users");
        });

        modelBuilder.Entity<UserMealFood>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MealId).HasColumnName("mealId");
            entity.Property(e => e.QuantityInGrams)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("quantityInGrams");
            entity.Property(e => e.UserFoodId).HasColumnName("userFoodId");

            entity.HasOne(d => d.Meal).WithMany(p => p.UserMealFoods)
                .HasForeignKey(d => d.MealId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMealFoods_Meals");

            entity.HasOne(d => d.UserFood).WithMany(p => p.UserMealFoods)
                .HasForeignKey(d => d.UserFoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMealFoods_UserFoods");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
