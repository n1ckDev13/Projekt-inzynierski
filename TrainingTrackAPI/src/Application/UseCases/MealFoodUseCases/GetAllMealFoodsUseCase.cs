using ClassLibrary.DTOs.MealFoodsDTOs;
using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.MealFoodUseCases
{
    public class GetAllMealFoodsUseCase : IGetAllMealFoodsUseCase
    {
        private readonly IMealsRepository _mealsRepository;
        private readonly IMealFoodsRepository _mealFoodsRepository;
        private readonly IFoodsRepository _foodsRepository;
        private readonly ICalculateTotalCaloriesAndMacrosService _calculateTotalCaloriesAndMacrosService;

        public GetAllMealFoodsUseCase(IMealsRepository mealsRepository,
            IMealFoodsRepository mealFoodsRepository,
            IFoodsRepository foodsRepository,
            ICalculateTotalCaloriesAndMacrosService calculateTotalCaloriesAndMacrosService)
        {
            _mealsRepository = mealsRepository;
            _mealFoodsRepository = mealFoodsRepository;
            _foodsRepository = foodsRepository;
            _calculateTotalCaloriesAndMacrosService = calculateTotalCaloriesAndMacrosService;
        }

        public async Task<GetAllMealFoodsResponse> GetAllMealFoodsAsync(int mealId)
        {
            try
            {
                var meal = await _mealsRepository.CheckIfMealExists(mealId);

                if (meal is null)
                    return new GetAllMealFoodsResponse(false, "Meal does not exist.", null, null);

                var mealFoodList = await _mealFoodsRepository.GetAllMealFoodsForMeal(mealId);

                if (mealFoodList is null)
                    return new GetAllMealFoodsResponse(false, "No data returned.", null, null);

                var mealFoodsDTOs = new List<GetAllMealFoodsDTO>();

                foreach (var mealFood in mealFoodList)
                {
                    var food = await _foodsRepository.CheckIfFoodExists(mealFood.FoodId);

                    decimal totalCalories = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                        mealFood.QuantityInGrams, food.CaloriesPer100g);

                    decimal totalProtein = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                        mealFood.QuantityInGrams, food.ProteinPer100g);

                    decimal totalCarbs = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                        mealFood.QuantityInGrams, food.CarbsPer100g);

                    decimal totalFat = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                        mealFood.QuantityInGrams, food.FatPer100g);

                    mealFoodsDTOs.Add(new GetAllMealFoodsDTO
                    {
                        Id = mealFood.Id,
                        MealId = mealFood.MealId,
                        FoodId = mealFood.FoodId,
                        QuantityInGrams = mealFood.QuantityInGrams,
                        FoodName = food.FoodName,
                        TotalCalories = totalCalories,
                        TotalProtein = totalProtein,
                        TotalCarbs = totalCarbs,
                        TotalFat = totalFat
                    });
                }

                return new GetAllMealFoodsResponse(true, "Data returned.", mealFoodsDTOs, null);
            }
            catch (Exception e)
            {
                var errors = new List<string>();
                errors.Add(e.Message);
                return new GetAllMealFoodsResponse(false, "Database error.", null, errors);
            }
        }
    }
}
