namespace ClassLibrary.DTOs.MealFoodsDTOs
{
    public class GetAllMealFoodsDTO
    {
        public int Id { get; set; }

        public int MealId { get; set; }

        public int FoodId { get; set; }

        public decimal QuantityInGrams { get; set; }

        public string FoodName { get; set; } = null!;

        public decimal TotalCalories { get; set; }

        public decimal TotalProtein { get; set; }

        public decimal TotalCarbs { get; set; }

        public decimal TotalFat { get; set; }
    }
}
