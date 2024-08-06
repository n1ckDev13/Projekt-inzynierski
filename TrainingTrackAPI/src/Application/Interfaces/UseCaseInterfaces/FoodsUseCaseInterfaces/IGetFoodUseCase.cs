using ClassLibrary.Responses.Food;
using TrainingTrackAPI.Application.UseCases.FoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.FoodsUseCaseInterfaces
{
    public interface IGetFoodUseCase
    {
        Task<GetFoodResponse> GetFoodAsync(int id);
    }
}
