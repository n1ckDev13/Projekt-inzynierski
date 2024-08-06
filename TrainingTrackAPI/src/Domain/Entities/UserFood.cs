namespace TrainingTrackAPI.Domain.Entities;

public partial class UserFood
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string FoodName { get; set; } = null!;

    public decimal CaloriesPer100g { get; set; }

    public decimal ProteinPer100g { get; set; }

    public decimal CarbsPer100g { get; set; }

    public decimal FatPer100g { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserMealFood> UserMealFoods { get; set; } = new List<UserMealFood>();
}
