using ClassLibrary.DTOs.MealFoodsDTOs;
using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces;
using TrainingTrackAPI.Application.Services.MealFoodServices;
using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Application.UseCases.MealFoodUseCases
{
    public class CreateMealFoodUseCase : ICreateMealFoodUseCase
    {
        private readonly IMealsRepository _mealsRepository;
        private readonly IFoodsRepository _foodsRepository;
        private readonly CreateMealFoodValidationService _mealFoodValidationService;
        private readonly IMealFoodsRepository _mealFoodsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMealFoodUseCase(IMealsRepository mealsRepository, 
            IFoodsRepository foodsRepository, 
            CreateMealFoodValidationService mealFoodValidationService,
            IMealFoodsRepository mealFoodsRepository,
            IUnitOfWork unitOfWork)
        {
            _mealsRepository = mealsRepository;
            _foodsRepository = foodsRepository;
            _mealFoodValidationService = mealFoodValidationService;
            _mealFoodsRepository = mealFoodsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateMealFoodResponse> CreateMealFoodAsync(CreateMealFoodDTO createMealFoodDTO)
        {
            var meal = await _mealsRepository.CheckIfMealExists(createMealFoodDTO.MealId);

            if (meal is null)
                return new CreateMealFoodResponse(false, "Meal does not exist.", null);

            var food = await _foodsRepository.CheckIfFoodExists(createMealFoodDTO.FoodId);

            if (food is null)
                return new CreateMealFoodResponse(false, "Food does not exist.", null);

            var validationResult = _mealFoodValidationService.Validate(createMealFoodDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new CreateMealFoodResponse(false, "Validation error.", errorMessages);
            }

            var mealFood = new MealFood
            {
                MealId = createMealFoodDTO.MealId,
                FoodId = createMealFoodDTO.FoodId,
                QuantityInGrams = createMealFoodDTO.QuantityInGrams
            };

            try
            {
                await _mealFoodsRepository.CreateAsync(mealFood);

                await _unitOfWork.CommitAsync();

                string details = $"New MealFood's id: {mealFood.Id}";
                List<string> detailsList = new List<string> { details };
                return new CreateMealFoodResponse(true, "MealFood creation successful.", detailsList);
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new CreateMealFoodResponse(false, "Database error.", errors);
            }
        }
    }
}
