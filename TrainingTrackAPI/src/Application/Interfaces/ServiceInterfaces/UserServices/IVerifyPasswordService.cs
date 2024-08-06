namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices
{
    public interface IVerifyPasswordService
    {
        public bool VerifyPassword(string passwordToCheck, string userPassword);
    }
}
