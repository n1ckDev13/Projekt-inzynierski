using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserMealFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.Application.Services.UserMealFoodServices
{
    public class GetUserMealFoodService : IGetUserMealFoodService
    {
        private readonly IGetUserMealFoodUseCase _getUserMealFoodUseCase;

        public GetUserMealFoodService(IGetUserMealFoodUseCase getUserMealFoodUseCase)
        {
            _getUserMealFoodUseCase = getUserMealFoodUseCase;
        }

        public async Task<GetUserMealFoodResponse> GetUserMealFoodAsync(int id)
        {
            return await _getUserMealFoodUseCase.GetUserMealFoodAsync(id);
        }
    }
}
