namespace ClassLibrary.Responses.User
{
    public class UpdateUserResponse
    {
        public bool isSuccess { get; set; } 
        public string Message { get; set; } = string.Empty;

        public string Token { get; set; }
        public List<string> Details { get; set; }

        public UpdateUserResponse(bool success, string message, string token, List<string> details)
        {
            isSuccess = success;
            Message = message;
            Token = token ?? string.Empty;
            Details = details ?? new List<string>();
        }
    }
}
