namespace ClassLibrary.DTOs.UserDTOs
{
    public class RegisterUserDTO
    {
        public string userName { get; set; } = null!;
        public string userMail { get; set; } = null!;
        public string password { get; set; } = null!;
        public string confirmPassword { get; set; } = null!;
    }
}
