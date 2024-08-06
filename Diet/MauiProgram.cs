
using ApiServiceAccess;
using ClassLibrary.Responses.User;
using CommunityToolkit.Maui;

using Microsoft.Extensions.Logging;


namespace Diet;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif


        builder.Services.AddSingleton<IApiAccess, ApiAccess>();

       
        
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<Food>();
        builder.Services.AddTransient<UserFoodPage>();
        builder.Services.AddTransient<ProfilePage>();
        

        builder.UseMauiApp<App>().UseMauiCommunityToolkit();
        
        return builder.Build();
    }
}