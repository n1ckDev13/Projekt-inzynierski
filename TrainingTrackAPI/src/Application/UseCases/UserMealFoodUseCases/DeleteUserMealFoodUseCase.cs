using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases
{
    public class DeleteUserMealFoodUseCase : IDeleteUserMealFoodUseCase
    {
        private readonly IUserMealFoodsRepository _userMealFoodsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserMealFoodUseCase(IUserMealFoodsRepository userMealFoodsRepository, IUnitOfWork unitOfWork)
        {
            _userMealFoodsRepository = userMealFoodsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteUserMealFoodResponse> DeleteUserMealFoodAsync(int id)
        {
            var userMealFood = await _userMealFoodsRepository.CheckIfUserMealFoodExist(id);

            if (userMealFood is null)
                return new DeleteUserMealFoodResponse(false, "UserMealFood does not exist.", null);

            try
            {
                await using (var transaction = await _unitOfWork.BeginTransactionAsync())
                {
                    await _userMealFoodsRepository.DeleteUserMealFood(id);

                    await _unitOfWork.CommitAsync();
                    await transaction.CommitAsync();

                    string details = $"Deleted MealFood's id: {id}";
                    List<string> detailsList = new List<string> { details };

                    return new DeleteUserMealFoodResponse(true, "UserMealFood deleted successfully.", detailsList);
                }


            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new DeleteUserMealFoodResponse(false, "Database error.", errors);
            }
        }
    }
}
