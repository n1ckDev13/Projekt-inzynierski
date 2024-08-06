using ClassLibrary.DTOs.MealFoodsDTOs;
using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealFoodServices
{
    public interface IUpdateMealFoodService
    {
        Task<UpdateMealFoodResponse> UpdateMealFoodAsync(UpdateMealFoodDTO updateMealFoodDTO);
    }
}
