using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserFoodUseCases;

namespace TrainingTrackAPI.Application.Services.UserFoodServices
{
    public class GetUserFoodService : IGetUserFoodService
    {
        private readonly IGetUserFoodUseCase _getUserFoodUseCase;

        public GetUserFoodService(IGetUserFoodUseCase getUserFoodUseCase)
        {
            _getUserFoodUseCase = getUserFoodUseCase;
        }

        public async Task<GetUserFoodResponse> GetUserFoodAsync(int id)
        {
            return await _getUserFoodUseCase.GetUserFoodAsync(id);
        }
    }
}
