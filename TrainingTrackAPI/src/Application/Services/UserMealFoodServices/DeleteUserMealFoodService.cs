using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserMealFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.Application.Services.UserMealFoodServices
{
    public class DeleteUserMealFoodService : IDeleteUserMealFoodService
    {
        private readonly IDeleteUserMealFoodUseCase _deleteUserMealFoodUseCase;

        public DeleteUserMealFoodService(IDeleteUserMealFoodUseCase deleteUserMealFoodUseCase)
        {
            _deleteUserMealFoodUseCase = deleteUserMealFoodUseCase;
        }

        public async Task<DeleteUserMealFoodResponse> DeleteUserMealFoodAsync(int id)
        {
            return await _deleteUserMealFoodUseCase.DeleteUserMealFoodAsync(id);
        }
    }
}
