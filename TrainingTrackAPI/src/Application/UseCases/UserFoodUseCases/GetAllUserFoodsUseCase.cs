using ClassLibrary.DTOs.UserFoodDTOs;
using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.UserFoodUseCases
{
    public class GetAllUserFoodsUseCase : IGetAllUserFoodsUseCase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUserFoodsRepository _userFoodsRepository;

        public GetAllUserFoodsUseCase(IUsersRepository usersRepository, IUserFoodsRepository userFoodsRepository)
        {
            _usersRepository = usersRepository;
            _userFoodsRepository = userFoodsRepository;
        }

        public async Task<GetAllUserFoodsResponse> GetAllUserFoodsAsync(int userId)
        {
            
            try
            {
                var user = await _usersRepository.CheckIfUserExists(userId);

                if (user is null)
                    return new GetAllUserFoodsResponse(false, "User does not exist.", null, null);

                var userFoodsList = await _userFoodsRepository.GetAllUserFoodsForUser(userId);

                if (userFoodsList is null)
                    return new GetAllUserFoodsResponse(false, "No data returned.", null, null);

                var userFoodsDTOs = userFoodsList.Select(userFood => new GetAllUserFoodDTO
                {
                    Id = userFood.Id,
                    UserId = userFood.UserId,
                    FoodName = userFood.FoodName,
                    CaloriesPer100g = userFood.CaloriesPer100g,
                    ProteinPer100g = userFood.ProteinPer100g,
                    CarbsPer100g = userFood.CarbsPer100g,
                    FatPer100g = userFood.FatPer100g
                }).ToList();

                return new GetAllUserFoodsResponse(true, "Data returned.", userFoodsDTOs, null);
            }
            catch(Exception e)
            {
                var errors = new List<string>();
                errors.Add(e.Message);
                return new GetAllUserFoodsResponse(false, "Database error.", null, errors);
            }
        }
    }
}
