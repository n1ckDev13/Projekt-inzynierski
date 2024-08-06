using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;

namespace TrainingTrackAPI.Application.Services.MealFoodServices
{
    public class GetAllMealFoodsService : IGetAllMealFoodsService
    {
        private readonly IGetAllMealFoodsUseCase _getAllMealFoodsUseCase;

        public GetAllMealFoodsService(IGetAllMealFoodsUseCase getAllMealFoodsUseCase)
        {
            _getAllMealFoodsUseCase = getAllMealFoodsUseCase;
        }

        public async Task<GetAllMealFoodsResponse> GetAllMealFoodsAsync(int mealId)
        {
            return await _getAllMealFoodsUseCase.GetAllMealFoodsAsync(mealId);
        }
    }
}
