using ClassLibrary.DTOs.UserFoodDTOs;

namespace ClassLibrary.Responses.UserFood
{
    public class GetAllUserFoodsResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<GetAllUserFoodDTO> UserFoodsList { get; set; }
        public List<string> Details { get; set; }

        public GetAllUserFoodsResponse(bool success, string message, List<GetAllUserFoodDTO> userFoodList, 
            List<string> details)
        {
            isSuccess = success;
            Message = message;
            UserFoodsList = userFoodList ?? new List<GetAllUserFoodDTO>();
            Details = details ?? new List<string>();
        }
    }
}
