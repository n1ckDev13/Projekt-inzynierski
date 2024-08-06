using ClassLibrary.DTOs.UserFoodDTOs;
using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.Services.UserFoodServices;
using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Application.UseCases.UserFoodUseCases
{
    public class CreateUserFoodUseCase : ICreateUserFoodUseCase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly CreateUserFoodValidationService _createUserFoodValidationService;
        private readonly ICalculateMacrosToCaloriesService _calculateMacrosToCaloriesService;
        private readonly IUserFoodsRepository _userFoodsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserFoodUseCase(IUsersRepository usersRepository, 
            CreateUserFoodValidationService createUserFoodValidationService,
            ICalculateMacrosToCaloriesService calculateMacrosToCaloriesService,
            IUserFoodsRepository userFoodsRepository, 
            IUnitOfWork unitOfWork)
        {
            _usersRepository = usersRepository;
            _createUserFoodValidationService = createUserFoodValidationService;
            _calculateMacrosToCaloriesService = calculateMacrosToCaloriesService;
            _userFoodsRepository = userFoodsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateUserFoodResponse> CreateUserFoodAsync(CreateUserFoodDTO createUserFoodDTO)
        {
            var user = await _usersRepository.CheckIfUserExists(createUserFoodDTO.UserId);

            if (user is null)
                return new CreateUserFoodResponse(false, "User does not exist.", null);

            var validationResult = _createUserFoodValidationService.Validate(createUserFoodDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new CreateUserFoodResponse(false, "Validation error.", errorMessages);
            }

            var proteinCalories = _calculateMacrosToCaloriesService.CalculateProteinOrCarbsToCalories(
                createUserFoodDTO.ProteinPer100g);

            var carbsCalories = _calculateMacrosToCaloriesService.CalculateProteinOrCarbsToCalories(
                createUserFoodDTO.CarbsPer100g);

            var fatCalories = _calculateMacrosToCaloriesService.CalculateFatToCalories(
                createUserFoodDTO.FatPer100g);

            var caloriesFromMacros =  Math.Round((proteinCalories + carbsCalories + fatCalories), 2);

            var userFood = new UserFood
            {
                UserId = createUserFoodDTO.UserId,
                FoodName = createUserFoodDTO.FoodName,
                CaloriesPer100g = caloriesFromMacros,
                ProteinPer100g = Math.Round(createUserFoodDTO.ProteinPer100g, 2),
                CarbsPer100g = Math.Round(createUserFoodDTO.CarbsPer100g, 2),
                FatPer100g = Math.Round(createUserFoodDTO.FatPer100g, 2)
            };


            try
            {
                await _userFoodsRepository.CreateAsync(userFood);

                await _unitOfWork.CommitAsync();

                string details = $"New UserFood's id: {userFood.Id}";
                List<string> detailsList = new List<string> { details };
                return new CreateUserFoodResponse(true, "UserFood creation successful.", detailsList);
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new CreateUserFoodResponse(false, "Database error.", errors);
            }
        }
    }
}
