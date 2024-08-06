using ClassLibrary.DTOs.MealDTO;
using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces;
using TrainingTrackAPI.Application.Services.MealServices;
using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Application.UseCases.MealUseCases
{
    public class CreateMealUseCase : ICreateMealUseCase
    {
        private readonly IDietPlansRepository _dietPlansRepository;
        private readonly CreateMealValidationService _createMealValidationService;
        private readonly IMealsRepository _mealsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMealUseCase(CreateMealValidationService createMealValidationService, 
            IDietPlansRepository dietPlansRepository, IMealsRepository mealsRepository, 
            IUnitOfWork unitOfWork)
        {
            _createMealValidationService = createMealValidationService;
            _dietPlansRepository = dietPlansRepository;
            _mealsRepository = mealsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateMealResponse> CreateMealAsync(CreateMealDTO createMealDTO)
        {
            var dietPlan = await _dietPlansRepository.CheckIfPlanExists(createMealDTO.DietPlanId);

            if (dietPlan is null)
                return new CreateMealResponse(false, "Diet plan does not exist.", null);

            var validationResult = _createMealValidationService.Validate(createMealDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new CreateMealResponse(false, "Validation error.", errorMessages);
            }

            var meal = new Meal
            {
                DietPlanId = createMealDTO.DietPlanId,
                TimeOfEating = createMealDTO.TimeOfEating,
                MealName = createMealDTO.MealName
            };


            try
            {
                await _mealsRepository.CreateAsync(meal);

                await _unitOfWork.CommitAsync();

                string details = $"New Meal's id: {meal.Id}";
                List<string> detailsList = new List<string> { details };
                return new CreateMealResponse(true, "Meal creation successful.", detailsList);
            }
            catch(Exception e)
            {
                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new CreateMealResponse(false, "Database error.", errors);
            }
        }
    }
}
