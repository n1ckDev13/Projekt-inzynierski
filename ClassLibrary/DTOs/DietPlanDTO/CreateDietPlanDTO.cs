namespace ClassLibrary.DTOs.DietPlanDTO;

    public class CreateDietPlanDTO 
    {
        public int UserId { get; set; }

        public string PlanName { get; set; } = null!;

        public decimal Calories { get; set; }

        public decimal Protein { get; set; }

        public decimal Carbs { get; set; }

        public decimal Fat { get; set; }

        
    }

