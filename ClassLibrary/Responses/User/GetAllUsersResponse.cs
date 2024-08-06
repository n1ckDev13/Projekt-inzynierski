using ClassLibrary.DTOs.UserDTOs;

namespace ClassLibrary.Responses.User
{
    public class GetAllUsersResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<GetAllUsersDTO> UsersList { get; set; }
        public List<string> Details { get; set; }

        public GetAllUsersResponse(bool success, string message, List<GetAllUsersDTO> usersList, List<string> details)
        {
            isSuccess = success;
            Message = message;
            UsersList = usersList ?? new List<GetAllUsersDTO>();
            Details = details ?? new List<string>();
        }
    }
}
