using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserFoodUseCases;

namespace TrainingTrackAPI.Application.Services.UserFoodServices
{
    public class GetAllUserFoodsService : IGetAllUserFoodsService
    {
        private readonly IGetAllUserFoodsUseCase _getAllUserFoodsUseCase;

        public GetAllUserFoodsService(IGetAllUserFoodsUseCase getAllUserFoodsUseCase)
        {
            _getAllUserFoodsUseCase = getAllUserFoodsUseCase;
        }

        public async Task<GetAllUserFoodsResponse> GetAllUserFoodsAsync(int userId)
        {
            return await _getAllUserFoodsUseCase.GetAllUserFoodsAsync(userId);
        }
    }
}
