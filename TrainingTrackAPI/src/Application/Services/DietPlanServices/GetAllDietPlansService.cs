using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.DietPlanServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.DietPlansUseCases;

namespace TrainingTrackAPI.Application.Services.DietPlanServices
{
    public class GetAllDietPlansService : IGetAllDietPlansService
    {
        private readonly IGetAllDietPlansUseCase _getAllDietPlansUseCase;

        public GetAllDietPlansService(IGetAllDietPlansUseCase getAllDietPlansUseCase)
        {
            _getAllDietPlansUseCase = getAllDietPlansUseCase;
        }

        public async Task<GetAllDietPlansResponse> GetAllDietPlansAsync(int userId)
        {
            return await _getAllDietPlansUseCase.GetAllDietPlansAsync(userId);
        }
    }
}
