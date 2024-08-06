using ClassLibrary.DTOs.FoodDTOs;
using ClassLibrary.Responses.Food;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.FoodsUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.FoodUseCases
{
    public class GetAllFoodsUseCase : IGetAllFoodsUseCase
    {
        private readonly IFoodsRepository _repo;

        public GetAllFoodsUseCase(IFoodsRepository repo)
        {
            _repo = repo;
        }

        public async Task<GetAllFoodsResponse> GetAllFoodsAsync()
        {
            try
            {

                var foodsList = await _repo.GetAllAsync();

                if (foodsList is null)
                    return new GetAllFoodsResponse(false, "No data returned.", null, null);

                var foodDTOs = foodsList.Select(food => new GetAllFoodsDTO
                {
                    Id = food.Id,
                    FoodName = food.FoodName,
                    CaloriesPer100g = food.CaloriesPer100g,
                    ProteinPer100g = food.ProteinPer100g,
                    CarbsPer100g = food.CarbsPer100g,
                    FatPer100g = food.FatPer100g
                }).ToList();

                return new GetAllFoodsResponse(true, "Data returned.", foodDTOs, null);

            }
            catch(Exception e)
            {
                var errors = new List<string>();
                errors.Add(e.Message);
                return new GetAllFoodsResponse(false, "Database error.", null, errors);
            }
        }
    }
}
