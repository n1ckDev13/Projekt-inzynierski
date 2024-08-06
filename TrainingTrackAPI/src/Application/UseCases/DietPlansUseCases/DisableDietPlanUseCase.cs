using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.DietPlansUseCases
{
    public class DisableDietPlanUseCase : IDisableDietPlanUseCase
    {
        private readonly IDietPlansRepository _dietPlansRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DisableDietPlanUseCase(IDietPlansRepository dietPlansRepository, IUnitOfWork unitOfWork)
        {
            _dietPlansRepository = dietPlansRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DisableDietPlanResponse> DisableDietPlanAsync(int id)
        {
            var dietPlan = await _dietPlansRepository.CheckIfPlanExists(id);

            if (dietPlan is null)
                return new DisableDietPlanResponse(false, "Diet plan does not exist.", null);

            try
            {
                 _dietPlansRepository.DisableDietPlan(id);

                await _unitOfWork.CommitAsync();

                string details = $"Disabled Diet Plan at id: {id}";
                List<string> detailsList = new List<string> { details };

                return new DisableDietPlanResponse(true, "Diet plan disabled.", detailsList);
            }
            catch(Exception e)
            {
                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new DisableDietPlanResponse(false, "Database error.", errors);
            }
        }
    }
}
