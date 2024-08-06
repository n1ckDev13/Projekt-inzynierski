using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserMealFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.Application.Services.UserMealFoodServices
{
    public class GetAllUserMealFoodsService : IGetAllUserMealFoodsService
    {
        private readonly IGetAllUserMealFoodsUseCase _getAllUserMealFoodsUseCase;

        public GetAllUserMealFoodsService(IGetAllUserMealFoodsUseCase getAllUserMealFoodsUseCase)
        {
            _getAllUserMealFoodsUseCase = getAllUserMealFoodsUseCase;
        }

        public async Task<GetAllUserMealFoodsResponse> GetAllUserMealFoodAsync(int mealId)
        {
            return await _getAllUserMealFoodsUseCase.GetAllUserMealFoodsAsync(mealId);
        }
    }
}
