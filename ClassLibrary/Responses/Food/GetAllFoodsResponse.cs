using ClassLibrary.DTOs.FoodDTOs;

namespace ClassLibrary.Responses.Food
{
    public class GetAllFoodsResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<GetAllFoodsDTO> FoodList { get; set; }
        public List<string> Details { get; set; }

        public GetAllFoodsResponse(bool success, string message, List<GetAllFoodsDTO> foodList, List<string> details)
        {
            isSuccess = success;
            Message = message;
            FoodList = foodList ?? new List<GetAllFoodsDTO>();
            Details = details ?? new List<string>();
        }
    }
}
