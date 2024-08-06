using ClassLibrary.DTOs.UserFoodDTOs;
using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.UserFoodUseCases
{
    public class GetUserFoodUseCase : IGetUserFoodUseCase
    {
        private readonly IUserFoodsRepository _userFoodsRepository;

        public GetUserFoodUseCase(IUserFoodsRepository userFoodsRepository)
        {
            _userFoodsRepository = userFoodsRepository;
        }

        public async Task<GetUserFoodResponse> GetUserFoodAsync(int id)
        {
            try
            {
                var userFood = await _userFoodsRepository.CheckIfUserFoodExists(id);

                if (userFood is null)
                    return new GetUserFoodResponse(false, "UserFood does not exist.", null, null);

                var userFoodResult = new GetAllUserFoodDTO();
                userFoodResult.Id = userFood.Id;
                userFoodResult.UserId = userFood.UserId;
                userFoodResult.FoodName = userFood.FoodName;
                userFoodResult.CaloriesPer100g = userFood.CaloriesPer100g;
                userFoodResult.ProteinPer100g = userFood.ProteinPer100g;
                userFoodResult.CarbsPer100g = userFood.CarbsPer100g;
                userFoodResult.FatPer100g = userFood.FatPer100g;

                return new GetUserFoodResponse(true, "Data returned.", userFoodResult, null);
            }
            catch(Exception e)
            {
                var errors = new List<string>();
                errors.Add(e.Message);
                return new GetUserFoodResponse(false, "Database error.", null, errors);
            }
        }
    }
}
