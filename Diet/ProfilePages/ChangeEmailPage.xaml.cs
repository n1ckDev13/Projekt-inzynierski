using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiServiceAccess;
using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;

namespace Diet.ProfilePages;

public partial class ChangeEmailPage : ContentPage
{
    private readonly IApiAccess _apiAccess;
    private UserCredentialsDTO _userCredentials;
    
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
    
    public ChangeEmailPage(IApiAccess apiAccess, UserCredentialsDTO userCredentials)
    {
        _apiAccess = apiAccess;
        _userCredentials = userCredentials;
        InitializeComponent();
    }
    
    private async void OnEditButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
            string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text))
        {
            await DisplayAlert("Błąd", "Uzupełnij wszystkie pola!", "OK");
            return;
        }
        
        IsProcessing = true;
        Indicator.IsRunning = true;

        var updateUser = new UpdateUserDTO()
        {
            Id = _userCredentials.Id,
            UserName = _userCredentials.UserName,
            UserEmail = EmailEntry.Text,
            UserPassword = PasswordEntry.Text,
            ConfirmUserPassword = ConfirmPasswordEntry.Text,
            NewPassword = String.Empty
            
        };

        

        UpdateUserResponse response = await _apiAccess.UpdateUserData(updateUser);

        string errorMessage = string.Join(Environment.NewLine, response.Details);

        Indicator.IsRunning = false;
        
        
        if (!response.isSuccess)
        {
            await DisplayAlert(response.Message, errorMessage, "OK");
        }
        else if(response.isSuccess)
        {
            await DisplayAlert("Sukces", "Zmieniono e-mail.", "OK");
        }

        IsProcessing = false;
    }
}