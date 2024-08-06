using ClassLibrary.DTOs.UserMealFoodDTOs;

namespace ClassLibrary.Responses.UserMealFood
{
    public class GetAllUserMealFoodsResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<GetAllUserMealFoodDTO> UserMealFoodsList { get; set; }
        public List<string> Details { get; set; }

        public GetAllUserMealFoodsResponse(bool success, string message, List<GetAllUserMealFoodDTO> userMealFoodsList, List<string> details)
        {
            isSuccess = success;
            Message = message;
            UserMealFoodsList = userMealFoodsList ?? new List<GetAllUserMealFoodDTO>();
            Details = details ?? new List<string>();
        }
    }
}
