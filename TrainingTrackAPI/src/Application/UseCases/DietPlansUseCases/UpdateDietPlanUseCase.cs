using ClassLibrary.DTOs.DietPlanDTO;
using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces;
using TrainingTrackAPI.Application.Services.DietPlanServices;

namespace TrainingTrackAPI.Application.UseCases.DietPlansUseCases
{
    public class UpdateDietPlanUseCase : IUpdateDietPlanUseCase
    {

        private readonly IDietPlansRepository _dietPlansRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UpdateDietPlanValidationService _updateDietPlanValidationService;
        private readonly ICalculateMacrosService _calculateMacrosService;

        public UpdateDietPlanUseCase(IDietPlansRepository dietPlansRepository,
            IUnitOfWork unitOfWork,
            UpdateDietPlanValidationService updateDietPlanValidationService,
            ICalculateMacrosService calculateMacrosService)
        {
            _dietPlansRepository = dietPlansRepository;
            _unitOfWork = unitOfWork;
            _updateDietPlanValidationService = updateDietPlanValidationService;
            _calculateMacrosService = calculateMacrosService;
        }

        public async Task<UpdateDietPlanResponse> UpdateDietPlanAsync(UpdateDietPlanDTO updateDietPlanDTO)
        {
            var dietPlan = await _dietPlansRepository.CheckIfPlanExists(updateDietPlanDTO.Id);

            if (dietPlan is null)
                return new UpdateDietPlanResponse(false, "Diet plan does not exist.", null);

            var validationResult = _updateDietPlanValidationService.Validate(updateDietPlanDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new UpdateDietPlanResponse(false, "Validation error.", errorMessages);
            }

            var macroValueCheck = updateDietPlanDTO.Protein + updateDietPlanDTO.Carbs +
                updateDietPlanDTO.Fat;

            if (macroValueCheck != 100)
                return new UpdateDietPlanResponse(false, "Macro percentages are not equal to 100%.", null);

            decimal proteinGrams = _calculateMacrosService.CalculateMacros(updateDietPlanDTO.Calories,
                updateDietPlanDTO.Protein, 4);

            decimal carbGrams = _calculateMacrosService.CalculateMacros(updateDietPlanDTO.Calories, updateDietPlanDTO.Carbs, 4);

            decimal fatGrams = _calculateMacrosService.CalculateMacros(updateDietPlanDTO.Calories, updateDietPlanDTO.Fat, 9);

            try
            {
                 _dietPlansRepository.UpdateDietPlan(updateDietPlanDTO.Id, updateDietPlanDTO.PlanName,
                    updateDietPlanDTO.Calories, proteinGrams, carbGrams, fatGrams);

                 await _unitOfWork.CommitAsync();

                string details = $"Updated Diet Plan's id: {updateDietPlanDTO.Id}";
                List<string> detailsList = new List<string> { details };

                return new UpdateDietPlanResponse(true, "Diet plan updated.", detailsList);
            }
            catch(Exception e)
            {
                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new UpdateDietPlanResponse(false, "Database error.", errors);
            }
        }
    }
}
