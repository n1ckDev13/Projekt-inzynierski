using ClassLibrary.DTOs.UserMealFoodDTOs;
using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.Services.UserMealFoodServices;
using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases
{
    public class CreateUserMealFoodUseCase : ICreateUserMealFoodUseCase
    {
        private readonly IMealsRepository _mealsRepository;
        private readonly IUserFoodsRepository _userFoodsRepository;
        private readonly CreateUserMealFoodValidationService _createUserMealFoodValidationService;
        private readonly IUserMealFoodsRepository _userMealFoodsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserMealFoodUseCase(IMealsRepository mealsRepository,
            IUserFoodsRepository userFoodsRepository,
            CreateUserMealFoodValidationService createUserMealFoodValidationService,
            IUserMealFoodsRepository userMealFoodsRepository,
            IUnitOfWork unitOfWork)
        {
            _mealsRepository = mealsRepository;
            _userFoodsRepository = userFoodsRepository;
            _createUserMealFoodValidationService = createUserMealFoodValidationService;
            _userMealFoodsRepository = userMealFoodsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateUserMealFoodResponse> CreateUserMealFoodAsync(CreateUserMealFoodDTO createUserMealFoodDTO)
        {
            var meal = await _mealsRepository.CheckIfMealExists(createUserMealFoodDTO.MealId);

            if (meal is null)
                return new CreateUserMealFoodResponse(false, "Meal does not exist.", null);
            var food = await _userFoodsRepository.CheckIfUserFoodExists(createUserMealFoodDTO.UserFoodId);

            if (food is null)
                return new CreateUserMealFoodResponse(false, "UserFood does not exist.", null);

            var validationResult = _createUserMealFoodValidationService.Validate(createUserMealFoodDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new CreateUserMealFoodResponse(false, "Validation error.", errorMessages);
            }

            var userMealFood = new UserMealFood
            {
                MealId = createUserMealFoodDTO.MealId,
                UserFoodId = createUserMealFoodDTO.UserFoodId,
                QuantityInGrams = createUserMealFoodDTO.QuantityInGrams
            };

            try
            {
                await _userMealFoodsRepository.CreateAsync(userMealFood);

                await _unitOfWork.CommitAsync();

                string details = $"New UserMealFood's id: {userMealFood.Id}";
                List<string> detailsList = new List<string> { details };
                return new CreateUserMealFoodResponse(true, "UserMealFood creation successful.", detailsList);
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new CreateUserMealFoodResponse(false, "Database error.", errors);
            }
        }
    }
}
