using ClassLibrary.DTOs.MealDTO;

namespace ClassLibrary.Responses.Meal
{
    public class GetMealResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public GetAllMealsDTO Meal { get; set; }
        public List<string> Details { get; set; }

        public GetMealResponse(bool success, string message, GetAllMealsDTO meal, List<string> details)
        {
            isSuccess = success;
            Message = message;
            Meal = meal ?? new GetAllMealsDTO();
            Details = details ?? new List<string>();
        }
    }
}
