using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;

namespace TrainingTrackAPI.Application.Services.MealFoodServices
{
    public class GetMealFoodService : IGetMealFoodService
    {
        private readonly IGetMealFoodUseCase _getMealFoodUseCase;

        public GetMealFoodService(IGetMealFoodUseCase getMealFoodUseCase)
        {
            _getMealFoodUseCase = getMealFoodUseCase;
        }

        public async Task<GetMealFoodResponse> GetMealFoodAsync(int id)
        {
            return await _getMealFoodUseCase.GetMealFoodAsync(id);
        }
    }
}
