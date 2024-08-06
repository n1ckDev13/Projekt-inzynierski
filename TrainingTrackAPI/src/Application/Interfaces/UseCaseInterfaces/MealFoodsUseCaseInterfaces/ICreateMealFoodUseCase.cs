using ClassLibrary.DTOs.MealFoodsDTOs;
using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces
{
    public interface ICreateMealFoodUseCase
    {
        Task<CreateMealFoodResponse> CreateMealFoodAsync(CreateMealFoodDTO createMealFoodDTO);
    }
}
