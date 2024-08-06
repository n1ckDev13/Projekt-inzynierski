using ClassLibrary.DTOs.MealDTO;
using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.MealUseCases;

namespace TrainingTrackAPI.Application.Services.MealServices
{
    public class CreateMealService : ICreateMealService
    {
        private readonly ICreateMealUseCase _createMealUseCase;

        public CreateMealService(ICreateMealUseCase createMealUseCase)
        {
            _createMealUseCase = createMealUseCase;
        }

        public async Task<CreateMealResponse> CreateMealAsync(CreateMealDTO createMealDTO)
        {
            return await _createMealUseCase.CreateMealAsync(createMealDTO);
        }
    }
}
