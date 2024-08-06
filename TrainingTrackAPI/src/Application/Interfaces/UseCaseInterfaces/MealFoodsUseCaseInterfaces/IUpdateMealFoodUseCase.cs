using ClassLibrary.DTOs.MealFoodsDTOs;
using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces
{
    public interface IUpdateMealFoodUseCase
    {
        Task<UpdateMealFoodResponse> UpdateMealFoodAsync(UpdateMealFoodDTO updateMealFoodDTO);
    }
}
