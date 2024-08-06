using ClassLibrary.DTOs.UserMealFoodDTOs;
using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserMealFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.Application.Services.UserMealFoodServices
{
    public class CreateUserMealFoodService : ICreateUserMealFoodService
    {
        private readonly ICreateUserMealFoodUseCase _createUserMealFoodUseCase;

        public CreateUserMealFoodService(ICreateUserMealFoodUseCase createUserMealFoodUseCase)
        {
            _createUserMealFoodUseCase = createUserMealFoodUseCase;
        }

        public async Task<CreateUserMealFoodResponse> CreateUserMealFoodAsync(CreateUserMealFoodDTO createUserMealFoodDTO)
        {
            return await _createUserMealFoodUseCase.CreateUserMealFoodAsync(createUserMealFoodDTO);
        }
    }
}
