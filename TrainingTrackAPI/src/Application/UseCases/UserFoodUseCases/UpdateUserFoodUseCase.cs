using ClassLibrary.DTOs.UserFoodDTOs;
using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.Services.UserFoodServices;

namespace TrainingTrackAPI.Application.UseCases.UserFoodUseCases
{
    public class UpdateUserFoodUseCase : IUpdateUserFoodUseCase
    {
        private readonly IUserFoodsRepository _userFoodsRepository;
        private readonly UpdateUserFoodValidationService _updateUserFoodValidationService;
        private readonly ICalculateMacrosToCaloriesService _calculateMacrosToCaloriesService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserFoodUseCase(IUserFoodsRepository userFoodsRepository,
            UpdateUserFoodValidationService updateUserFoodValidationService,
            ICalculateMacrosToCaloriesService calculateMacrosToCaloriesService, 
            IUnitOfWork unitOfWork)
        {
            _userFoodsRepository = userFoodsRepository;
            _updateUserFoodValidationService = updateUserFoodValidationService;
            _calculateMacrosToCaloriesService = calculateMacrosToCaloriesService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateUserFoodResponse> UpdateUserFoodAsync(UpdateUserFoodDTO updateUserFoodDTO)
        {
            var userFood = await _userFoodsRepository.CheckIfUserFoodExists(updateUserFoodDTO.Id);

            if (userFood is null)
                return new UpdateUserFoodResponse(false, "UserFood does not exist.", null);

            var validationResult = _updateUserFoodValidationService.Validate(updateUserFoodDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new UpdateUserFoodResponse(false, "Validation error.", errorMessages);
            }

            var proteinCalories = _calculateMacrosToCaloriesService.CalculateProteinOrCarbsToCalories(
                updateUserFoodDTO.ProteinPer100g);

            var carbsCalories = _calculateMacrosToCaloriesService.CalculateProteinOrCarbsToCalories(
                updateUserFoodDTO.CarbsPer100g);

            var fatCalories = _calculateMacrosToCaloriesService.CalculateFatToCalories(
            updateUserFoodDTO.FatPer100g);

            var caloriesFromMacros = Math.Round((proteinCalories + carbsCalories + fatCalories), 2);
            
            try
            {
                _userFoodsRepository.UpdateUserFood(updateUserFoodDTO.Id, updateUserFoodDTO.FoodName,
                    caloriesFromMacros, Math.Round(updateUserFoodDTO.ProteinPer100g, 2),
                    Math.Round(updateUserFoodDTO.CarbsPer100g, 2),
                    Math.Round(updateUserFoodDTO.FatPer100g, 2));

                await _unitOfWork.CommitAsync();

                string details = $"Updated UserFood's id: {updateUserFoodDTO.Id}";
                List<string> detailsList = new List<string> { details };

                return new UpdateUserFoodResponse(true, "UserFood updated.", detailsList);
            }
            catch (Exception e)
            {

                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new UpdateUserFoodResponse(false, "Database error.", errors);

            }
        }
    }
}
