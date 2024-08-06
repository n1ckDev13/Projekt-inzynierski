using ClassLibrary.DTOs.MealFoodsDTOs;

namespace ClassLibrary.Responses.MealFood
{
    public class GetAllMealFoodsResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<GetAllMealFoodsDTO> MealFoodsList { get; set; }
        public List<string> Details { get; set; }

        public GetAllMealFoodsResponse(bool success, string message, List<GetAllMealFoodsDTO> mealFoodsList, List<string> details)
        {
            isSuccess = success;
            Message = message;
            MealFoodsList = mealFoodsList ?? new List<GetAllMealFoodsDTO>();
            Details = details ?? new List<string>();
        }
    }
}
