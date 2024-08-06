using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.MealUseCases;

namespace TrainingTrackAPI.Application.Services.MealServices
{
    public class DeleteMealService : IDeleteMealService
    {
        private readonly IDeleteMealUseCase _deleteMealUseCase;

        public DeleteMealService(IDeleteMealUseCase deleteMealUseCase)
        {
            _deleteMealUseCase = deleteMealUseCase;
        }

        public async Task<DeleteMealResponse> DeleteMealAsync(int id)
        {
            return await _deleteMealUseCase.DeleteMealAsync(id);
        }
    }
}
