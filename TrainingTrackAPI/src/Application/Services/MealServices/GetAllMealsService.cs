using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.MealUseCases;

namespace TrainingTrackAPI.Application.Services.MealServices
{
    public class GetAllMealsService : IGetAllMealsService
    {
        private readonly IGetAllMealsUseCase _getAllMealsUseCase;

        public GetAllMealsService(IGetAllMealsUseCase getAllMealsUseCase)
        {
            _getAllMealsUseCase = getAllMealsUseCase;
        }

        public async Task<GetAllMealsResponse> GetAllMealsAsync(int dietPlanId)
        {
           return await _getAllMealsUseCase.GetAllMealsAsync(dietPlanId);
        }
    }
}
