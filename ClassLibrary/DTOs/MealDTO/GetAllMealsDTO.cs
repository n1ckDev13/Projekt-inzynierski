namespace ClassLibrary.DTOs.MealDTO
{
    public class GetAllMealsDTO
    {
        public int Id { get; set; }

        public int DietPlanId { get; set; }

        public TimeOnly TimeOfEating { get; set; }

        public string MealName { get; set; } = null!;

        
    }
}
