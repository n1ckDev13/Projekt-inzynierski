namespace TrainingTrackAPI.Domain.Entities;

public partial class UserMealFood
{
    public int Id { get; set; }

    public int MealId { get; set; }

    public int UserFoodId { get; set; }

    public decimal QuantityInGrams { get; set; }

    public virtual Meal Meal { get; set; } = null!;

    public virtual UserFood UserFood { get; set; } = null!;
}
