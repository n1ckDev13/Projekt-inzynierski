using ClassLibrary.Responses.Food;
using TrainingTrackAPI.Application.UseCases.FoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.FoodsUseCaseInterfaces
{
    public interface IGetAllFoodsUseCase
    {
        Task<GetAllFoodsResponse> GetAllFoodsAsync();
    }
}
