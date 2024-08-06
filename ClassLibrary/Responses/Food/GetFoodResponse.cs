
using ClassLibrary.DTOs.FoodDTOs;

namespace ClassLibrary.Responses.Food
{
    public class GetFoodResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public GetAllFoodsDTO Food { get; set; }
        public List<string> Details { get; set; }

        public GetFoodResponse(bool success, string message, GetAllFoodsDTO food, List<string> details)
        {
            isSuccess = success;
            Message = message;
            Food = food ?? new GetAllFoodsDTO();
            Details = details ?? new List<string>();
        }
    }
}
