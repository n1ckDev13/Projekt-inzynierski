using System.Diagnostics;
using ApiServiceAccess;

namespace Diet;

public partial class App : Application
{
    private readonly IApiAccess _apiAccess;
    
    public App(IApiAccess apiAccess)
    {
        _apiAccess = apiAccess;
        
        InitializeComponent();
        
        Preferences.Remove("AuthToken");

        _apiAccess.UnauthorizedRequest += OnUnauthorizedRequest;

        var token = Preferences.Get("AuthToken", null);
        Debug.WriteLine($"Token : {token}");

        MainPage = new NavigationPage(new LoginPage(_apiAccess));
        
        
    }

    private void OnUnauthorizedRequest(object sender, EventArgs e)
    {
        Preferences.Set("AuthToken", null);
        MainPage = new NavigationPage(new LoginPage(_apiAccess));
    }
}