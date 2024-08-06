using ClassLibrary.DTOs.UserMealFoodDTOs;
using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases
{
    public class GetUserMealFoodUseCase : IGetUserMealFoodUseCase
    {
        private readonly IUserMealFoodsRepository _userMealFoodsRepository;
        private readonly IUserFoodsRepository _userFoodRepository;
        private readonly ICalculateTotalCaloriesAndMacrosService _calculateTotalCaloriesAndMacrosService;

        public GetUserMealFoodUseCase(IUserMealFoodsRepository userMealFoodsRepository, 
            IUserFoodsRepository userFoodRepository, 
            ICalculateTotalCaloriesAndMacrosService calculateTotalCaloriesAndMacrosService)
        {
            _userMealFoodsRepository = userMealFoodsRepository;
            _userFoodRepository = userFoodRepository;
            _calculateTotalCaloriesAndMacrosService = calculateTotalCaloriesAndMacrosService;
        }

        public async Task<GetUserMealFoodResponse> GetUserMealFoodAsync(int id)
        {
            try
            {
                var userMealFood = await _userMealFoodsRepository.CheckIfUserMealFoodExist(id);

                if (userMealFood is null)
                    return new GetUserMealFoodResponse(false, "UserMealFood does not exist.", null, null);

                var userFood = await _userFoodRepository.CheckIfUserFoodExists(userMealFood.UserFoodId);

                var userMealFoodResult = new GetAllUserMealFoodDTO();
                userMealFoodResult.Id = userMealFood.Id;
                userMealFoodResult.MealId = userMealFood.MealId;
                userMealFoodResult.UserFoodId = userMealFood.UserFoodId;
                userMealFoodResult.QuantityInGrams = userMealFood.QuantityInGrams;
                userMealFoodResult.UserFoodName = userFood.FoodName;

                userMealFoodResult.TotalCalories = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                    userMealFood.QuantityInGrams, userFood.CaloriesPer100g);

                userMealFoodResult.TotalProtein = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                    userMealFood.QuantityInGrams, userFood.ProteinPer100g);

                userMealFoodResult.TotalCarbs = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                    userMealFood.QuantityInGrams, userFood.CarbsPer100g);

                userMealFoodResult.TotalFat = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                    userMealFood.QuantityInGrams, userFood.FatPer100g);

                return new GetUserMealFoodResponse(true, "Data returned.", userMealFoodResult, null);
            }
            catch (Exception e)
            {
                var errors = new List<string>();
                errors.Add(e.Message);
                return new GetUserMealFoodResponse(false, "Database error.", null, errors);
            }
        }
    }
}
