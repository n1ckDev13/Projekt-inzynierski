namespace ClassLibrary.DTOs.UserDTOs
{
    public class UserCredentialsDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string UserMail { get; set; } = null!;
    }
}
