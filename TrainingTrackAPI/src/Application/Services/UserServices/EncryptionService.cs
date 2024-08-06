using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices;

namespace TrainingTrackAPI.Application.Services.UserServices
{
    public class EncryptionService : IEncryptionService
    {
        public string HashPassword(string password)
        {
            

            var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);

            return hashedPassword;
        }

        public string GenerateSalt()
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();

            return salt;
        }

        public string HashPasswordToVerify(string passwordWithSalt)
        {
            var passwordToVerify = BCrypt.Net.BCrypt.EnhancedHashPassword(passwordWithSalt);

            return passwordToVerify;
        }
    }
}
