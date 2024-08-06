using ClassLibrary.DTOs.MealDTO;
using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.UseCases.MealUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces
{
    public interface IUpdateMealUseCase
    {
        Task<UpdateMealResponse> UpdateMealAsync(UpdateMealDTO updateMealDTO);
    }
}
