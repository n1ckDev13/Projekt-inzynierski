using ClassLibrary.DTOs.MealFoodsDTOs;
using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;

namespace TrainingTrackAPI.Application.Services.MealFoodServices
{
    public class CreateMealFoodService : ICreateMealFoodService
    {
        private readonly ICreateMealFoodUseCase _createMealFoodUseCase;

        public CreateMealFoodService(ICreateMealFoodUseCase createMealFoodUseCase)
        {
            _createMealFoodUseCase = createMealFoodUseCase;
        }

        public async Task<CreateMealFoodResponse> CreateMealFoodAsync(CreateMealFoodDTO createMealFoodDTO)
        {
            return await _createMealFoodUseCase.CreateMealFoodAsync(createMealFoodDTO);
        }
    }
}
