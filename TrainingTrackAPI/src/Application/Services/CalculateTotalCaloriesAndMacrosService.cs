using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService;

namespace TrainingTrackAPI.Application.Services
{
    public class CalculateTotalCaloriesAndMacrosService : ICalculateTotalCaloriesAndMacrosService
    {
        public decimal CalculateTotalCaloriesAndMacros(decimal quantityInGrams, decimal macroValue)
        {
            decimal multiplier = quantityInGrams / 100;

            return Math.Round((macroValue * multiplier), 2);
        }
    }
}
