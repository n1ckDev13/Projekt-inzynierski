namespace TrainingTrackAPI.Domain.Entities;

public partial class DietPlan
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string PlanName { get; set; } = null!;

    public bool IsDisabled { get; set; }

    public decimal Calories { get; set; }

    public decimal Protein { get; set; }

    public decimal Carbs { get; set; }

    public decimal Fat { get; set; }

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public virtual User User { get; set; } = null!;
}
