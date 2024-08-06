using ClassLibrary.Responses.Food;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.FoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.FoodsUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.FoodUseCases;

namespace TrainingTrackAPI.Application.Services.FoodsServices
{
    public class GetAllFoodsService : IGetAllFoodsService
    {
        private readonly IGetAllFoodsUseCase _getAllFoodsUseCase;

        public GetAllFoodsService(IGetAllFoodsUseCase getAllFoodsUseCase)
        {
            _getAllFoodsUseCase = getAllFoodsUseCase;
        }

        public async Task<GetAllFoodsResponse> GetAllFoodsAsync()
        {
            return await _getAllFoodsUseCase.GetAllFoodsAsync();
        }
    }
}
