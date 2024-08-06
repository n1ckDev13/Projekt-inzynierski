using ClassLibrary.DTOs.MealFoodsDTOs;
using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;

namespace TrainingTrackAPI.Application.Services.MealFoodServices
{
    public class UpdateMealFoodService : IUpdateMealFoodService
    {
        private readonly IUpdateMealFoodUseCase _updateMealFoodUseCase;

        public UpdateMealFoodService(IUpdateMealFoodUseCase updateMealFoodUseCase)
        {
            _updateMealFoodUseCase = updateMealFoodUseCase;
        }

        public async Task<UpdateMealFoodResponse> UpdateMealFoodAsync(UpdateMealFoodDTO updateMealFoodDTO)
        {
            return await _updateMealFoodUseCase.UpdateMealFoodAsync(updateMealFoodDTO);
        }
    }
}
