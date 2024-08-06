namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MacroService
{
    public interface ICalculateTotalCaloriesAndMacrosService
    {
        decimal CalculateTotalCaloriesAndMacros(decimal calories, decimal macroValue);
    }
}
