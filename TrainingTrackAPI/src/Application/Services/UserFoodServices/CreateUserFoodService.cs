using ClassLibrary.DTOs.UserFoodDTOs;
using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserFoodUseCases;

namespace TrainingTrackAPI.Application.Services.UserFoodServices
{
    public class CreateUserFoodService : ICreateUserFoodService
    {
        private readonly ICreateUserFoodUseCase _createUserFoodUseCase;

        public CreateUserFoodService(ICreateUserFoodUseCase createUserFoodUseCase)
        {
            _createUserFoodUseCase = createUserFoodUseCase;
        }

        public async Task<CreateUserFoodResponse> CreateUserFoodAsync(CreateUserFoodDTO createUserFoodDTO)
        {
            return await _createUserFoodUseCase.CreateUserFoodAsync(createUserFoodDTO);
        }
    }
}
