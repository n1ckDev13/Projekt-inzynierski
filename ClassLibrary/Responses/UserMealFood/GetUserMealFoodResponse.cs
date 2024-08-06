using ClassLibrary.DTOs.UserMealFoodDTOs;

namespace ClassLibrary.Responses.UserMealFood
{
    public class GetUserMealFoodResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public GetAllUserMealFoodDTO userMealFood { get; set; }
        public List<string> Details { get; set; }

        public GetUserMealFoodResponse(bool success, string message, GetAllUserMealFoodDTO userMealFood, List<string> details)
        {
            isSuccess = success;
            Message = message;
            this.userMealFood = userMealFood ?? new GetAllUserMealFoodDTO();
            Details = details ?? new List<string>();
        }
    }
}
