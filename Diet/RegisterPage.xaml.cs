using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiServiceAccess;
using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;

namespace Diet;

public partial class RegisterPage : ContentPage
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
    
    public RegisterPage(IApiAccess apiAccess)
    {
        _apiAccess = apiAccess;
        
        InitializeComponent();

        BindingContext = this;
    }

    private async void OnCreateAccountButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(UserNameEntry.Text) ||
            string.IsNullOrWhiteSpace(UserMailEntry.Text) ||
            string.IsNullOrWhiteSpace(PassowordEntry.Text) ||
            string.IsNullOrWhiteSpace(ConfirmPassowrdEntry.Text))
        {
            await DisplayAlert("Błąd", "Uzupełnij wszystkie pola!", "OK");
            return;
        }
        
        IsProcessing = true;
        Indicator.IsRunning = true;

        var newAccountCredentials = new RegisterUserDTO()
        {
            userName = UserNameEntry.Text,
            userMail = UserMailEntry.Text,
            password = PassowordEntry.Text,
            confirmPassword = ConfirmPassowrdEntry.Text
        };

        

        RegisterUserResponse response = await _apiAccess.RegisterUser(newAccountCredentials);

        string errorMessage = string.Join(Environment.NewLine, response.Details);

        Indicator.IsRunning = false;
        
        
        if (!response.isSuccess)
        {
            await DisplayAlert(response.Message, errorMessage, "OK");
        }
        else if(response.isSuccess)
        {
            await DisplayAlert("Sukces", "Utworzono konto.", "OK");
        }

        IsProcessing = false;
    }
}