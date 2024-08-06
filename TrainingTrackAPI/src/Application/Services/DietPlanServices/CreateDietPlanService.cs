using ClassLibrary.DTOs.DietPlanDTO;
using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.DietPlanServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.DietPlansUseCases;

namespace TrainingTrackAPI.Application.Services.DietPlanServices;

public class CreateDietPlanService : ICreateDietPlanService
{
    private readonly ICreateDietPlanUseCase _createDietPlanUseCase;

    public CreateDietPlanService(ICreateDietPlanUseCase createDietPlanUseCase)
    {
        _createDietPlanUseCase = createDietPlanUseCase;
    }

    public async Task<CreateDietPlanResponse> CreateDietPlanAsync(CreateDietPlanDTO createDietPlanDTO)
    {
        return await _createDietPlanUseCase.CreateDietPlanAsync(createDietPlanDTO);
    }
}