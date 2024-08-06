using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;

namespace TrainingTrackAPI.Application.Services.MealFoodServices
{
    public class DeleteMealFoodService : IDeleteMealFoodService
    {
        private readonly IDeleteMealFoodUseCase _deleteMealFoodUseCase;

        public DeleteMealFoodService(IDeleteMealFoodUseCase deleteMealFoodUseCase)
        {
            _deleteMealFoodUseCase = deleteMealFoodUseCase;
        }

        public async Task<DeleteMealFoodResponse> DeleteMealFoodAsync(int id)
        {
            return await _deleteMealFoodUseCase.DeleteMealFoodAsync(id);
        }
    }
}
