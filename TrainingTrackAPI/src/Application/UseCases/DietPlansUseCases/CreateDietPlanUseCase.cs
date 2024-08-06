using ClassLibrary.DTOs.DietPlanDTO;
using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces;
using TrainingTrackAPI.Application.Services.DietPlanServices;
using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Application.UseCases.DietPlansUseCases
{
    public class CreateDietPlanUseCase : ICreateDietPlanUseCase
    {
        private readonly DietPlanValidationService _dietPlanValidationService;
        private readonly ICalculateMacrosService _calculateMacrosService;
        private readonly IUsersRepository _usersRepository;
        private readonly IDietPlansRepository _dietPlansRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDietPlanUseCase(DietPlanValidationService dietPlanValidationService,
            ICalculateMacrosService calculateMacrosService,
            IUsersRepository usersRepository, IDietPlansRepository dietPlansRepository,
                IUnitOfWork unitOfWork)
        {
            _dietPlanValidationService = dietPlanValidationService;
            _calculateMacrosService = calculateMacrosService;
            _usersRepository = usersRepository;
            _dietPlansRepository = dietPlansRepository;
            _unitOfWork =unitOfWork;
        }

        public async Task<CreateDietPlanResponse> CreateDietPlanAsync(CreateDietPlanDTO createDietPlanDTO)
        {
            var user = await _usersRepository.CheckIfUserExists(createDietPlanDTO.UserId);

            if (user is null)
                return new CreateDietPlanResponse(false, "User does not exist", null);

            var validationResult = _dietPlanValidationService.Validate(createDietPlanDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new CreateDietPlanResponse(false, "Validation error.", errorMessages);
            }

            var macroValueCheck = createDietPlanDTO.Protein + createDietPlanDTO.Carbs +
                createDietPlanDTO.Fat;

            if (macroValueCheck != 100)
                return new CreateDietPlanResponse(false, "Macro percentages are not equal to 100%.", null);


            decimal proteinGrams = _calculateMacrosService.CalculateMacros(createDietPlanDTO.Calories,
                createDietPlanDTO.Protein, 4);

            decimal carbGrams = _calculateMacrosService.CalculateMacros(createDietPlanDTO.Calories, createDietPlanDTO.Carbs, 4);
            
            decimal fatGrams = _calculateMacrosService.CalculateMacros(createDietPlanDTO.Calories, createDietPlanDTO.Fat, 9);

            var dietPlanEntity = new DietPlan
            {
                UserId = createDietPlanDTO.UserId,
                PlanName = createDietPlanDTO.PlanName,
                IsDisabled = false,
                Calories = createDietPlanDTO.Calories,
                Protein = proteinGrams,
                Carbs = carbGrams,
                Fat = fatGrams

            };


            try
            {
                await _dietPlansRepository.CreateAsync(dietPlanEntity);

                await _unitOfWork.CommitAsync();

                string details = $"New Diet Plan's id: {dietPlanEntity.Id}";
                List<string> detailsList = new List<string> { details };
                return new CreateDietPlanResponse(true, "Diet plan creation successful.", detailsList);
            }
            catch (Exception e) 
            {
                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new CreateDietPlanResponse(false, "Database error.", errors);
            }

        }


      
    }
}
