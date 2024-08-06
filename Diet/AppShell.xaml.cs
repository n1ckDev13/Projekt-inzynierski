using ApiServiceAccess;

namespace Diet;

public partial class AppShell : Shell
{
    private readonly IApiAccess _apiAccess;
    
    public AppShell(IApiAccess apiAccess)
    {
        _apiAccess = apiAccess;
        InitializeComponent();
        
    }
}