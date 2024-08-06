using ClassLibrary.DTOs.MealFoodsDTOs;

namespace ClassLibrary.Responses.MealFood
{
    public class GetMealFoodResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public GetAllMealFoodsDTO mealFood { get; set; }
        public List<string> Details { get; set; }

        public GetMealFoodResponse(bool success, string message, GetAllMealFoodsDTO mealFood, List<string> details)
        {
            isSuccess = success;
            Message = message;
            this.mealFood = mealFood ?? new GetAllMealFoodsDTO();
            Details = details ?? new List<string>();
        }
    }
}
