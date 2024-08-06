namespace ClassLibrary.DTOs.UserMealFoodDTOs
{
    public class CreateUserMealFoodDTO
    {
        public int MealId { get; set; }

        public int UserFoodId { get; set; }

        public decimal QuantityInGrams { get; set; }
    }
}
