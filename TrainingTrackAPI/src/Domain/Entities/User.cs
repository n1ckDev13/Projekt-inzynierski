namespace TrainingTrackAPI.Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string UserMail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public byte[]? ProfilePicture { get; set; }

    public virtual ICollection<DietPlan> DietPlans { get; set; } = new List<DietPlan>();

    public virtual ICollection<UserFood> UserFoods { get; set; } = new List<UserFood>();
}
