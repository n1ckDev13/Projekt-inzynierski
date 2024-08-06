namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices
{
    public interface IJwtTokenService
    {
        public string GenerateToken(string userID, string userName, string userMail);
    }
}
