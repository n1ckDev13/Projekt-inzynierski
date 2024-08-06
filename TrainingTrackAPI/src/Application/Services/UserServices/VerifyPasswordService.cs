using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices;

namespace TrainingTrackAPI.Application.Services.UserServices
{
    public class VerifyPasswordService : IVerifyPasswordService
    {
        public bool VerifyPassword(string passwordToCheck, string userPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(passwordToCheck, userPassword);
        }
    }
}
