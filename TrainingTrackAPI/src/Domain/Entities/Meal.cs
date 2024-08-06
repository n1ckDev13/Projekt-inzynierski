namespace TrainingTrackAPI.Domain.Entities;

public partial class Meal
{
    public int Id { get; set; }

    public int DietPlanId { get; set; }

    public TimeOnly TimeOfEating { get; set; }

    public string MealName { get; set; } = null!;

    public virtual DietPlan DietPlan { get; set; } = null!;

    public virtual ICollection<MealFood> MealFoods { get; set; } = new List<MealFood>();

    public virtual ICollection<UserMealFood> UserMealFoods { get; set; } = new List<UserMealFood>();
}
