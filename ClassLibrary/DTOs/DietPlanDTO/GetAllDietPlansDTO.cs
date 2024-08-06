namespace ClassLibrary.DTOs.DietPlanDTO
{
    public class GetAllDietPlansDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string PlanName { get; set; } = null!;

        public bool IsDisabled { get; set; }

        public decimal Calories { get; set; }

        public decimal Protein { get; set; }

        public decimal Carbs { get; set; }

        public decimal Fat { get; set; }
    }
}
