namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService
{
    public interface ICalculateMacrosToCaloriesService
    {
        decimal CalculateProteinOrCarbsToCalories(decimal macroGrams);
        decimal CalculateFatToCalories(decimal macroGrams);
        
    }
}
