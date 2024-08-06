using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ApiServiceAccess;
using ClassLibrary.DTOs.UserDTOs;
using Diet.ProfilePages;

namespace Diet;

public partial class ProfilePage : ContentPage
{
    private readonly IApiAccess _apiAccess;
    private UserCredentialsDTO _userData;
    public ICommand RefreshingCommand { get; }
    
    private bool _isPageLoading = true;

    public bool IsPageLoading
    {
        get => _isPageLoading;
        set
        {
            if (_isPageLoading == value) return;

            _isPageLoading = value;
            OnPropertyChanged();
        }
    }
    
    public ProfilePage(IApiAccess apiAccess)
    {
        _apiAccess = apiAccess;
        
        InitializeComponent();
        RefreshingCommand = new Command(() => LoadPageContent());
        BindingContext = this;
        LoadPageContent();
    }
    
    private async void LoadPageContent()
    {
       
        
        IsPageLoading = true;

        _userData = _apiAccess.GetUserCredentials();

        UserNameLabel.Text = _userData.UserName;
        UserMailLabel.Text = _userData.UserMail;
        
        IsPageLoading = false;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        
        LoadPageContent();
    }

    private async void OnChangeUserNameButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ChangeUserNamePage(_apiAccess, _userData));
    }
    
    private async void OnChangeEmailButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ChangeEmailPage(_apiAccess, _userData));
    }
    
    private async void OnChangePasswordButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ChangePasswordPage(_apiAccess, _userData));
    }
    
    private async void OnDeactivateAccountButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new DeactivateAccountPage(_apiAccess, _userData));
    }
    
    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        _apiAccess.SetAuthToken(null);
        if (Application.Current != null)
            Application.Current.MainPage =  new NavigationPage(new LoginPage(_apiAccess));
    }

}