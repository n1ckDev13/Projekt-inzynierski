using ClassLibrary.DTOs.DietPlanDTO;
using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.DietPlansUseCases
{
    public class GetAllDietPlansUseCase : IGetAllDietPlansUseCase
    {
        private readonly IDietPlansRepository _dietPlansRepository;
        private readonly IUsersRepository _usersRepository;

        public GetAllDietPlansUseCase(IDietPlansRepository dietPlansRepository,
            IUsersRepository usersRepository)
        {
            _dietPlansRepository = dietPlansRepository;
            _usersRepository = usersRepository;
        }

        public async Task<GetAllDietPlansResponse> GetAllDietPlansAsync(int userId)
        {
            try
            {

                var user = await _usersRepository.CheckIfUserExists(userId);

                if (user is null)
                    return new GetAllDietPlansResponse(false, "User does not exist.", null, null);

                var dietPlansList = await _dietPlansRepository.GetAllDietPlans(userId);

                if (dietPlansList is null)
                    return new GetAllDietPlansResponse(false, "No data returned.", null, null);

                var dietPlanDTOs = dietPlansList.Select(dietPlan => new GetAllDietPlansDTO
                {
                    Id = dietPlan.Id,
                    UserId = dietPlan.UserId,
                    PlanName = dietPlan.PlanName,
                    IsDisabled = dietPlan.IsDisabled,
                    Calories = dietPlan.Calories,
                    Protein = dietPlan.Protein,
                    Carbs = dietPlan.Carbs,
                    Fat = dietPlan.Fat
                }).ToList();

                return new GetAllDietPlansResponse(true, "Data returned.", dietPlanDTOs, null);

            }
            catch(Exception e)
            {
                var errors = new List<string>();
                errors.Add(e.Message);
                return new GetAllDietPlansResponse(false, "Database error.", null, errors);
            }
        }
    }
}
