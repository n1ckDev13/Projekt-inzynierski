using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.UseCases.UserFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserFoodServices
{
    public interface IGetAllUserFoodsService
    {
        Task<GetAllUserFoodsResponse> GetAllUserFoodsAsync(int userId);
    }
}
