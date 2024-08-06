using ClassLibrary.DTOs.UserFoodDTOs;

namespace ClassLibrary.Responses.UserFood
{
    public class GetUserFoodResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public GetAllUserFoodDTO UserFood { get; set; }
        public List<string> Details { get; set; }

        public GetUserFoodResponse(bool success, string message, GetAllUserFoodDTO userFood, List<string> details)
        {
            isSuccess = success;
            Message = message;
            UserFood = userFood ?? new GetAllUserFoodDTO();
            Details = details ?? new List<string>();
        }
    }
}
