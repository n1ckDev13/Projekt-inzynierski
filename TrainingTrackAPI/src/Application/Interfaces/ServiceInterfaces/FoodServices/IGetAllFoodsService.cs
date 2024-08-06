using ClassLibrary.Responses.Food;
using TrainingTrackAPI.Application.UseCases.FoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.FoodServices
{
    public interface IGetAllFoodsService
    {
        Task<GetAllFoodsResponse> GetAllFoodsAsync();
    }
}
