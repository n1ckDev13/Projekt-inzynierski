namespace ClassLibrary.DTOs.UserDTOs
{
    public class NewUserTokenDataDTO
    {
        public string id = null!;
        public string userName = null!;
        public string userMail = null!;


        public NewUserTokenDataDTO(string id, string userName, string userMail)
        {
            this.id = id;
            this.userName = userName;
            this.userMail = userMail;
        }
    }
}
