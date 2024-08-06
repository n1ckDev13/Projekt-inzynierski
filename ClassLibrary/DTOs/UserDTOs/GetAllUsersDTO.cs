namespace ClassLibrary.DTOs.UserDTOs
{
    public class GetAllUsersDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string UserMail { get; set; } = null!;

        public string Password { get; set; } = null!;


        public bool IsActive { get; set; }

        public byte[]? ProfilePicture { get; set; }
    }
}
