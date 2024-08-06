namespace ClassLibrary.DTOs.UserDTOs
{
    public class DeactivateUserDTO
    {
        public int id {  get; set; }
        public string password { get; set; } = null!;

        public string confirmPassword { get; set; } = null!;
    }
}
