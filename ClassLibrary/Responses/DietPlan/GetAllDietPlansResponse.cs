using ClassLibrary.DTOs.DietPlanDTO;

namespace ClassLibrary.Responses.DietPlan
{
    public class GetAllDietPlansResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<GetAllDietPlansDTO> dietPlansList { get; set; }
        public List<string> Details { get; set; }

        public GetAllDietPlansResponse(bool success, string message, List<GetAllDietPlansDTO> dietPlansList, List<string> details)
        {
            isSuccess = success;
            Message = message;
            this.dietPlansList = dietPlansList ?? new List<GetAllDietPlansDTO>();
            Details = details ?? new List<string>();
        }
    }
}
