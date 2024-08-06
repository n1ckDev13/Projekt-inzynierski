using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces
{
    public interface IGetAllUsersUseCase
    {
        Task<GetAllUsersResponse> GetAllUsersAsync();
    }
}
