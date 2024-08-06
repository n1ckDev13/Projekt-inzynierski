namespace TrainingTrackAPI.Domain.Entities;

public partial class MealFood
{
    public int Id { get; set; }

    public int MealId { get; set; }

    public int FoodId { get; set; }

    public decimal QuantityInGrams { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual Meal Meal { get; set; } = null!;
}
