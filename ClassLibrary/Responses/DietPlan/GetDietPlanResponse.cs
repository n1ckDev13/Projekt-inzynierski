using ClassLibrary.DTOs.DietPlanDTO;

namespace ClassLibrary.Responses.DietPlan
{
    public class GetDietPlanResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public GetAllDietPlansDTO dietPlan { get; set; }
        public List<string> Details { get; set; }

        public GetDietPlanResponse(bool success, string message, GetAllDietPlansDTO dietPlan, List<string> details)
        {
            isSuccess = success;
            Message = message;
            this.dietPlan = dietPlan ?? new GetAllDietPlansDTO();
            Details = details ?? new List<string>();
        }
    }
}
