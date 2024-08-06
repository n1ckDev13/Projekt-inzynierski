using ClassLibrary.DTOs.MealFoodsDTOs;
using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.MealFoodUseCases
{
    public class GetMealFoodUseCase : IGetMealFoodUseCase
    {
        private readonly IMealFoodsRepository _mealFoodsRepository;
        private readonly IFoodsRepository _foodsRepository;
        private readonly ICalculateTotalCaloriesAndMacrosService _calculateTotalCaloriesAndMacrosService;

        public GetMealFoodUseCase(IMealFoodsRepository mealFoodsRepository, 
                IFoodsRepository foodsRepository,
                ICalculateTotalCaloriesAndMacrosService calculateTotalCaloriesAndMacrosService)
        {
            _mealFoodsRepository = mealFoodsRepository;
            _foodsRepository = foodsRepository;
            _calculateTotalCaloriesAndMacrosService = calculateTotalCaloriesAndMacrosService;
        }

        public async Task<GetMealFoodResponse> GetMealFoodAsync(int id)
        {
            try
            {
                var mealFood = await _mealFoodsRepository.CheckIfMealFoodExist(id);

                if (mealFood is null)
                    return new GetMealFoodResponse(false, "MealFood does not exist.", null, null);

                var food = await _foodsRepository.CheckIfFoodExists(mealFood.FoodId);

                var mealFoodResult = new GetAllMealFoodsDTO();
                mealFoodResult.Id = mealFood.Id;
                mealFoodResult.MealId = mealFood.MealId;
                mealFoodResult.FoodId = mealFood.FoodId;
                mealFoodResult.QuantityInGrams = mealFood.QuantityInGrams;
                mealFoodResult.FoodName = food.FoodName;

                mealFoodResult.TotalCalories = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                    mealFood.QuantityInGrams, food.CaloriesPer100g);

                mealFoodResult.TotalProtein = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                    mealFood.QuantityInGrams, food.ProteinPer100g);

                mealFoodResult.TotalCarbs = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                    mealFood.QuantityInGrams, food.CarbsPer100g);

                mealFoodResult.TotalFat = _calculateTotalCaloriesAndMacrosService.CalculateTotalCaloriesAndMacros(
                    mealFood.QuantityInGrams, food.FatPer100g);

                return new GetMealFoodResponse(true, "Data returned.", mealFoodResult, null);
            }
            catch(Exception e)
            {
                var errors = new List<string>();
                errors.Add(e.Message);
                return new GetMealFoodResponse(false, "Database error.", null, errors);
            }


        }
    }
}
