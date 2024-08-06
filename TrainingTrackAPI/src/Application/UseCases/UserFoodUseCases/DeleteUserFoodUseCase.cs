using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.UserFoodUseCases
{
    public class DeleteUserFoodUseCase : IDeleteUserFoodUseCase
    {
        private readonly IUserFoodsRepository _userFoodsRepository;
        private readonly IUserMealFoodsRepository _userMealFoodsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserFoodUseCase(IUserFoodsRepository userFoodsRepository, 
            IUserMealFoodsRepository userMealFoodsRepository, 
            IUnitOfWork unitOfWork)
        {
            _userFoodsRepository = userFoodsRepository;
            _userMealFoodsRepository = userMealFoodsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteUserFoodResponse> DeleteUserFoodAsync(int id)
        {
            var userFood = await _userFoodsRepository.CheckIfUserFoodExists(id);

            if (userFood is null)
                return new DeleteUserFoodResponse(false, "UserFood does not exist.", null);

            try
            {
               await using(var transaction = await _unitOfWork.BeginTransactionAsync())
                {

                   await _userFoodsRepository.DeleteUserFood(id);
                   await _userMealFoodsRepository.DeleteByUserFoodId(id);

                    await _unitOfWork.CommitAsync();
                    await transaction.CommitAsync();
                    

                    string details = $"Deleted UserFood's id: {id}";
                    List<string> detailsList = new List<string> { details };

                    return new DeleteUserFoodResponse(true, "UserFood deleted successfully.", detailsList);

                }
                

               
            }
            catch (Exception e)
            {

                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new DeleteUserFoodResponse(false, "Database error.", errors);
            }
        }
    }
}
