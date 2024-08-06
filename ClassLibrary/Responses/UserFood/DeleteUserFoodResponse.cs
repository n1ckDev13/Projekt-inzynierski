namespace ClassLibrary.Responses.UserFood
{
    public class DeleteUserFoodResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Details { get; set; }

        public DeleteUserFoodResponse(bool success, string message, List<string> details)
        {
            isSuccess = success;
            Message = message;
            Details = details ?? new List<string>();
        }
    }
}
