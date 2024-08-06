using ClassLibrary.Responses.Food;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.FoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.FoodsUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.FoodUseCases;

namespace TrainingTrackAPI.Application.Services.FoodsServices
{
    public class GetFoodService : IGetFoodService
    {
        private readonly IGetFoodUseCase _getFoodUseCase;

        public GetFoodService(IGetFoodUseCase getFoodUseCase)
        {
            _getFoodUseCase = getFoodUseCase;
        }

        public async Task<GetFoodResponse> GetFoodAsync(int id)
        {
            return await _getFoodUseCase.GetFoodAsync(id);
        }
    }
}
