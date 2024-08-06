using ClassLibrary.DTOs.UserMealFoodDTOs;
using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.Services.UserMealFoodServices;

namespace TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases
{
    public class UpdateUserMealFoodUseCase : IUpdateUserMealFoodUseCase
    {

        private readonly IUserMealFoodsRepository _userMealFoodsRepository;
        private readonly UpdateUserMealFoodValidationService _updateUserMealFoodValidationService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserMealFoodUseCase(IUserMealFoodsRepository userMealFoodsRepository,
            UpdateUserMealFoodValidationService updateUserMealFoodValidationService,
            IUnitOfWork unitOfWork)
        {
            _userMealFoodsRepository = userMealFoodsRepository;
            _updateUserMealFoodValidationService = updateUserMealFoodValidationService;
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdateUserMealFoodResponse> UpdateUserMealFoodAsync(UpdateUserMealFoodDTO updateUserMealFoodDTO)
        {
            var userMealFood = await _userMealFoodsRepository.CheckIfUserMealFoodExist(updateUserMealFoodDTO.Id);


            if (userMealFood is null)
                return new UpdateUserMealFoodResponse(false, "UserMealFood does not exist.", null);

            var validationResult = _updateUserMealFoodValidationService.Validate(updateUserMealFoodDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new UpdateUserMealFoodResponse(false, "Validation error.", errorMessages);
            }

            try
            {
                _userMealFoodsRepository.UpdateUserMealFood(updateUserMealFoodDTO.Id, 
                    updateUserMealFoodDTO.QuantityInGrams);

                await _unitOfWork.CommitAsync();

                string details = $"Updated UserMealFood's id: {updateUserMealFoodDTO.Id}";
                List<string> detailsList = new List<string> { details };

                return new UpdateUserMealFoodResponse(true, "UserMealFood updated.", detailsList);
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new UpdateUserMealFoodResponse(false, "Database error.", errors);
            }
        }
    }
}
