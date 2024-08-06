using ClassLibrary.DTOs.MealDTO;
using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.UseCases.MealUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealServices
{
    public interface IUpdateMealService
    {
        Task<UpdateMealResponse> UpdateMealAsync(UpdateMealDTO updateMealDTO);
    }
}
