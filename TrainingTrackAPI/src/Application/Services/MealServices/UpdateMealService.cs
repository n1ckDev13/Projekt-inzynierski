using ClassLibrary.DTOs.MealDTO;
using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.MealUseCases;

namespace TrainingTrackAPI.Application.Services.MealServices
{
    public class UpdateMealService : IUpdateMealService
    {
        private readonly IUpdateMealUseCase _updateMealUseCase;

        public UpdateMealService(IUpdateMealUseCase updateMealUseCase)
        {
            _updateMealUseCase = updateMealUseCase;
        }

        public async Task<UpdateMealResponse> UpdateMealAsync(UpdateMealDTO updateMealDTO)
        {
            return await _updateMealUseCase.UpdateMealAsync(updateMealDTO);
        }
    }
}
