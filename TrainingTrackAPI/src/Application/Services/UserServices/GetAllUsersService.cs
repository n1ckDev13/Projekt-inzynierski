using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.Application.Services.UserServices
{
    public class GetAllUsersService : IGetAllUsersService
    {
        private readonly IGetAllUsersUseCase _getAllUsersUseCase;

        public GetAllUsersService(IGetAllUsersUseCase getAllUsersUseCase)
        {
            _getAllUsersUseCase = getAllUsersUseCase;
        }

        public async Task<GetAllUsersResponse> GetAllUsersAsync()
        {
            return await _getAllUsersUseCase.GetAllUsersAsync();
        }
    }
}
