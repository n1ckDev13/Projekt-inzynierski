namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService
{
    public interface ICalculateMacrosService
    {
        decimal CalculateMacros(decimal totalCalories, decimal macroPercentage, decimal caloriesPerGram);

        
    }
}
