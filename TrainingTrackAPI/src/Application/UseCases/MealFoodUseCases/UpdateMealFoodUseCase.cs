using ClassLibrary.DTOs.MealFoodsDTOs;
using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces;
using TrainingTrackAPI.Application.Services.MealFoodServices;

namespace TrainingTrackAPI.Application.UseCases.MealFoodUseCases
{
    public class UpdateMealFoodUseCase : IUpdateMealFoodUseCase
    {
        private readonly IMealFoodsRepository _mealFoodsRepository;
        private readonly UpdateMealFoodValidationService _updateMealFoodValidationService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMealFoodUseCase(IMealFoodsRepository mealFoodsRepository,
            UpdateMealFoodValidationService updateMealFoodValidationService,
            IUnitOfWork unitOfWork)
        {
            _mealFoodsRepository = mealFoodsRepository;
            _updateMealFoodValidationService = updateMealFoodValidationService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateMealFoodResponse> UpdateMealFoodAsync(UpdateMealFoodDTO updateMealFoodDTO)
        {
            var mealFood =  await _mealFoodsRepository.CheckIfMealFoodExist(updateMealFoodDTO.Id);

            if (mealFood is null)
                return new UpdateMealFoodResponse(false, "MealFood does not exist.", null);

            var validationResult = _updateMealFoodValidationService.Validate(updateMealFoodDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new UpdateMealFoodResponse(false, "Validation error.", errorMessages);
            }

            try
            {
                _mealFoodsRepository.UpdateMealFood(updateMealFoodDTO.Id, updateMealFoodDTO.QuantityInGrams);
                
                await _unitOfWork.CommitAsync();

                string details = $"Updated MealFood's id: {updateMealFoodDTO.Id}";
                List<string> detailsList = new List<string> { details };

                return new UpdateMealFoodResponse(true, "MealFood updated.", detailsList);
            }
            catch(Exception e)
            {
                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new UpdateMealFoodResponse(false, "Database error.", errors);
            }
        }
    }
}
