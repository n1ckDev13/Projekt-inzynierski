using ClassLibrary.DTOs.FoodDTOs;
using ClassLibrary.Responses.Food;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.FoodsUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.FoodUseCases
{
    public class GetFoodUseCase : IGetFoodUseCase
    {
        private readonly IFoodsRepository _repo;

        public GetFoodUseCase(IFoodsRepository repo)
        {
            _repo = repo;
        }

        public async Task<GetFoodResponse> GetFoodAsync(int id)
        {
            try
            {

                var food = await _repo.CheckIfFoodExists(id);

                if (food is null)
                    return new GetFoodResponse(false, "Food does not exist.", null, null);

                var foodResult = new GetAllFoodsDTO();
                foodResult.Id = food.Id;
                foodResult.FoodName = food.FoodName;
                foodResult.CaloriesPer100g = food.CaloriesPer100g;
                foodResult.ProteinPer100g = food.ProteinPer100g;
                foodResult.CarbsPer100g = food.CarbsPer100g;
                foodResult.FatPer100g = food.FatPer100g;

                return new GetFoodResponse(true, "Data returned.", foodResult, null);
            }
            catch(Exception e)
            {
                var errors = new List<string>();
                errors.Add(e.Message);
                return new GetFoodResponse(false, "Database error.", null, errors);
            }
        }
    }
}
