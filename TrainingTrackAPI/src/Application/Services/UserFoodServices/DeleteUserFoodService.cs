using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserFoodUseCases;

namespace TrainingTrackAPI.Application.Services.UserFoodServices
{
    public class DeleteUserFoodService : IDeleteUserFoodService
    {
        private readonly IDeleteUserFoodUseCase _deleteUserFoodUseCase;

        public DeleteUserFoodService(IDeleteUserFoodUseCase deleteUserFoodUseCase)
        {
            _deleteUserFoodUseCase = deleteUserFoodUseCase;
        }

        public async Task<DeleteUserFoodResponse> DeleteUserFoodAsync(int id)
        {
            return await _deleteUserFoodUseCase.DeleteUserFoodAsync(id);
        }

        

      
    }
}
