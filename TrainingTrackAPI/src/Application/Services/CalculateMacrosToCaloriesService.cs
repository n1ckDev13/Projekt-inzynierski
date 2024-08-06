using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService;

namespace TrainingTrackAPI.Application.Services
{
    public class CalculateMacrosToCaloriesService : ICalculateMacrosToCaloriesService
    {
        public decimal CalculateFatToCalories(decimal macroGrams)
        {
            return macroGrams * 9;
        }

        public decimal CalculateProteinOrCarbsToCalories(decimal macroGrams)
        {
            return macroGrams * 4;
        }
    }
}
