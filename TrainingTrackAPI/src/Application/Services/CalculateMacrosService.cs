using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService;

namespace TrainingTrackAPI.Application.Services
{
    public class CalculateMacrosService : ICalculateMacrosService
    {
       

        public decimal CalculateMacros(decimal totalCalories, decimal macroPercentage, decimal caloriesPerGram)
        {
            decimal macroCalories = (macroPercentage / 100) * totalCalories;
            return Math.Round((macroCalories / caloriesPerGram), 2);
        }
    }
}
