using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices
{
    public interface IGetAllUsersService
    {
        Task<GetAllUsersResponse> GetAllUsersAsync();
    }
}
