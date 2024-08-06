using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.MealUseCases;

namespace TrainingTrackAPI.Application.Services.MealServices
{
    public class GetMealService : IGetMealService
    {
        private readonly IGetMealUseCase _getMealUseCase;

        public GetMealService(IGetMealUseCase getMealUseCase)
        {
            _getMealUseCase = getMealUseCase;
        }

        public async Task<GetMealResponse> GetMealAsync(int id)
        {
            return await _getMealUseCase.GetMealAsync(id);
        }
    }
}
