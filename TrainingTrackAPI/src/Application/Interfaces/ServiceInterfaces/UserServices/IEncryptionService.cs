namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices
{
    public interface IEncryptionService
    {
        string GenerateSalt();
        string HashPassword(string password);
        string HashPasswordToVerify(string password);
    }
}
