using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiServiceAccess;
using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;

namespace Diet;

public partial class LoginPage : ContentPage
{
    private bool _isProcessing = false;

    public bool IsProcessing
    {
        get => _isProcessing;
        set
        {
            if (_isProcessing == value) return;

            _isProcessing = value;
            OnPropertyChanged();
        }
    }
    
    
    private readonly IApiAccess _apiAccess;
    
    public LoginPage(IApiAccess apiAccess)
    {
        _apiAccess = apiAccess;
        
        InitializeComponent();

        BindingContext = this;
        
    }


    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        IsProcessing = true;
        await Navigation.PushAsync(new RegisterPage(_apiAccess));
        IsProcessing = false;
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        

        if (string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            await DisplayAlert("Błąd", "Uzupełnij wszystkie pola!", "OK");
            return;
        }
        
        IsProcessing = true;
        Indicator.IsRunning = true;

        var userCredentials = new LoginUserDTO()
        {
            UserMail = EmailEntry.Text,
            Password = PasswordEntry.Text
        };

        

        LoginUserResponse response = await _apiAccess.LoginUser(userCredentials);
        
        var token = Preferences.Get("AuthToken", null);
        Debug.WriteLine($"Token : {token}");

        Indicator.IsRunning = false;
        
        if (!response.isSuccess)
        {
            await DisplayAlert("Błąd", response.Message, "OK");
        }
        else if(response.isSuccess)
        {
            if (_apiAccess.GetAuthToken() != null)
            {
                Application.Current.MainPage = new AppShell(_apiAccess);
            }
        }

        IsProcessing = false;
    }
}