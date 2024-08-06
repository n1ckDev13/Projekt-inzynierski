using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.UseCases.MealUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces
{
    public interface IGetAllMealsUseCase
    {
        Task<GetAllMealsResponse> GetAllMealsAsync(int dietPlanId);
    }
}
