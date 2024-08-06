using ClassLibrary.DTOs.MealDTO;

namespace ClassLibrary.Responses.Meal
{
    public class GetAllMealsResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<GetAllMealsDTO> MealList { get; set; }
        public List<string> Details { get; set; }

        public GetAllMealsResponse(bool success, string message, List<GetAllMealsDTO> mealList, List<string> details)
        {
            isSuccess = success;
            Message = message;
            MealList = mealList ?? new List<GetAllMealsDTO>();
            Details = details ?? new List<string>();
        }
    }
}
