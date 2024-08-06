using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.MealUseCases
{
    public class DeleteMealUseCase : IDeleteMealUseCase
    {
        private readonly IMealsRepository _mealsRepository;
        private readonly IMealFoodsRepository _mealFoodsRepository;
        private readonly IUserMealFoodsRepository _userMealFoodsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMealUseCase(IMealsRepository mealsRepository, 
            IMealFoodsRepository mealFoodsRepository, 
            IUserMealFoodsRepository userMealFoodsRepository, 
            IUnitOfWork unitOfWork)
        {
            _mealsRepository = mealsRepository;
            _mealFoodsRepository = mealFoodsRepository;
            _userMealFoodsRepository = userMealFoodsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteMealResponse> DeleteMealAsync(int id)
        {
            var meal = await _mealsRepository.CheckIfMealExists(id);

            if (meal is null)
                return new DeleteMealResponse(false, "Meal does not exist.", null);


            try
            {
                await using (var transaction = await _unitOfWork.BeginTransactionAsync())
                {
                    await _mealsRepository.DeleteMeal(id);
                    await _userMealFoodsRepository.DeleteByMealId(id);
                    await _mealFoodsRepository.DeleteByMealId(id);

                    await _unitOfWork.CommitAsync();
                    await transaction.CommitAsync();

                    string details = $"Deleted Meal's id: {id}";
                    List<string> detailsList = new List<string> { details };

                    return new DeleteMealResponse(true, "Meal deleted successfully.", detailsList);
                }

                    
            }
            catch(Exception e)
            {

                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new DeleteMealResponse(false, "Database error.", errors);
            }

        }
    }
}
