using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.UseCases.MealUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces
{
    public interface IDeleteMealUseCase
    {
        Task<DeleteMealResponse> DeleteMealAsync(int id);
    }
}
