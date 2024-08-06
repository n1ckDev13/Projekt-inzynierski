namespace ClassLibrary.DTOs.UserMealFoodDTOs
{
    public class GetAllUserMealFoodDTO
    {
        public int Id { get; set; }

        public int MealId { get; set; }

        public int UserFoodId { get; set; }

        public decimal QuantityInGrams { get; set; }
        public string UserFoodName { get; set; } = null!;

        public decimal TotalCalories { get; set; }

        public decimal TotalProtein { get; set; }

        public decimal TotalCarbs { get; set; }

        public decimal TotalFat { get; set; }
    }
}
