using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.MealFoodUseCases
{
    public class DeleteMealFoodUseCase : IDeleteMealFoodUseCase
    {
        private readonly IMealFoodsRepository _mealFoodsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMealFoodUseCase(IMealFoodsRepository mealFoodsRepository, IUnitOfWork unitOfWork)
        {
            _mealFoodsRepository = mealFoodsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteMealFoodResponse> DeleteMealFoodAsync(int id)
        {
            var foodMeal = await _mealFoodsRepository.CheckIfMealFoodExist(id);

            if (foodMeal is null)
                return new DeleteMealFoodResponse(false, "MealFood does not exist.", null);

            try
            {
                await using (var transaction = await _unitOfWork.BeginTransactionAsync())
                {
                    await _mealFoodsRepository.DeleteMealFood(id);

                    await _unitOfWork.CommitAsync();
                    await transaction.CommitAsync();

                    string details = $"Deleted MealFood's id: {id}";
                    List<string> detailsList = new List<string> { details };

                    return new DeleteMealFoodResponse(true, "MealFood deleted successfully.", detailsList);
                }

                  
            }
            catch(Exception e)
            {
                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new DeleteMealFoodResponse(false, "Database error.", errors);
            }
        }
    }
}
