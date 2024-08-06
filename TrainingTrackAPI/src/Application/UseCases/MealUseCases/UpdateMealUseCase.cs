using ClassLibrary.DTOs.MealDTO;
using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces;
using TrainingTrackAPI.Application.Services.MealServices;

namespace TrainingTrackAPI.Application.UseCases.MealUseCases
{
    public class UpdateMealUseCase : IUpdateMealUseCase
    {
        private readonly IMealsRepository _mealsRepository;
        private readonly UpdateMealValidationService _updateMealValidationService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMealUseCase(IMealsRepository mealsRepository, 
            UpdateMealValidationService updateMealValidationService, 
            IUnitOfWork unitOfWork)
        {
            _mealsRepository = mealsRepository;
            _updateMealValidationService = updateMealValidationService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateMealResponse> UpdateMealAsync(UpdateMealDTO updateMealDTO)
        {
            var meal = await _mealsRepository.CheckIfMealExists(updateMealDTO.Id);

            if (meal is null)
                return new UpdateMealResponse(false, "Meal does not exist.", null);

            var validationResult = _updateMealValidationService.Validate(updateMealDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new UpdateMealResponse(false, "Validation error.", errorMessages);
            }


            try
            {
                _mealsRepository.UpdateMeal(updateMealDTO.Id, updateMealDTO.TimeOfEating, updateMealDTO.MealName);
               
                await _unitOfWork.CommitAsync();

                string details = $"Updated Meal's id: {updateMealDTO.Id}";
                List<string> detailsList = new List<string> { details };

                return new UpdateMealResponse(true, "Meal updated.", detailsList);
            }
            catch(Exception e) 
            {

                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new UpdateMealResponse(false, "Database error.", errors);

            }
        }
    }
}
