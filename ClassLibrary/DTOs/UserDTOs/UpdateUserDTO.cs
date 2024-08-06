namespace ClassLibrary.DTOs.UserDTOs
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;

        public string UserPassword { get; set; } = null!;
        public string ConfirmUserPassword { get; set; } = null!;

        public string NewPassword { get; set; } = null!;
        
    }
}
