using ClassLibrary.DTOs.UserMealFoodDTOs;
using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases
{
    public class GetAllUserMealFoodsUseCase : IGetAllUserMealFoodsUseCase
    {
        private readonly IMealsRepository _mealsRepository;
        private readonly IUserMealFoodsRepository _userMealFoodsRepository;
        private readonly IUserFoodsRepository _userFoodRepository;
        private readonly ICalculateTotalCaloriesAndMacrosService _calculateTotalCaloriesAndMacrosService;

        public GetAllUserMealFoodsUseCase(IMealsRepository mealsRepository, 
            IUserMealFoodsRepository userMealFoodsRepository, 
            IUserFoodsRepository userFoodRepository, 
            ICalculateTotalCaloriesAndMacrosService calculateTotalCaloriesAndMacrosService)
        {
            _mealsRepository = mealsRepository;
            _userMealFoodsRepository = userMealFoodsRepository;
            _userFoodRepository = userFoodRepository;
            _calculateTotalCaloriesAndMacrosService = calculateTotalCaloriesAndMacrosService;
        }

        public async Task<GetAllUserMealFoodsResponse> GetAllUserMealFoodsAsync(int mealId)
        {
            try
            {
                var meal = await _mealsRepository.CheckIfMealExists(mealId);

                if (meal is null)
                    return new GetAllUserMealFoodsResponse(false, "Meal does not exist.", null, null);

                var userMealFoodList = await _userMealFoodsRepository.GetAllUserMealFoodsForMeal(mealId);

                if (userMealFoodList is null)
                    return new GetAllUserMealFoodsResponse(false, "No data returned.", null, null);

                var userMealFoodsDTOs = new List<GetAllUserMealFoodDTO>();

                foreach (var userMealFood in userMealFoodList)
                {
                    var userFood = await _userFoodRepository.CheckIfUserFoodExists(userMealFood.UserFoodId);

                    decimal totalCalories = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                        userMealFood.QuantityInGrams, userFood.CaloriesPer100g);

                    decimal totalProtein = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                        userMealFood.QuantityInGrams, userFood.ProteinPer100g);

                    decimal totalCarbs = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                        userMealFood.QuantityInGrams, userFood.CarbsPer100g);

                    decimal totalFat = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                        userMealFood.QuantityInGrams, userFood.FatPer100g);

                    userMealFoodsDTOs.Add(new GetAllUserMealFoodDTO
                    {
                        Id = userMealFood.Id,
                        MealId = userMealFood.MealId,
                        UserFoodId = userMealFood.UserFoodId,
                        QuantityInGrams = userMealFood.QuantityInGrams,
                        UserFoodName = userFood.FoodName,
                        TotalCalories = totalCalories,
                        TotalProtein = totalProtein,
                        TotalCarbs = totalCarbs,
                        TotalFat = totalFat
                    });
                }

                return new GetAllUserMealFoodsResponse(true, "Data returned.", userMealFoodsDTOs, null);
            }
            catch (Exception e)
            {
                var errors = new List<string>();
                errors.Add(e.Message);
                return new GetAllUserMealFoodsResponse(false, "Database error.", null, errors);
            }
        }
    }
}
