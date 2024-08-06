using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiServiceAccess;
using ClassLibrary.DTOs.UserFoodDTOs;
using ClassLibrary.Responses.UserFood;

namespace Diet;

public partial class AddUserFoodPage : ContentPage
{
    private readonly IApiAccess _apiAccess;
    private int _userId;
    
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
    
    public AddUserFoodPage(IApiAccess apiAccess, int userId)
    {
        _apiAccess = apiAccess;
        _userId = userId;
        InitializeComponent();
    }
    
    private async void OnCreateButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FoodNameEntry.Text) ||
            string.IsNullOrWhiteSpace(ProteinEntry.Text) ||
            string.IsNullOrWhiteSpace(CarbsEntry.Text) ||
            string.IsNullOrWhiteSpace(FatEntry.Text))
        {
            await DisplayAlert("Błąd", "Uzupełnij wszystkie pola!", "OK");
            return;
        }
        
        IsProcessing = true;
        Indicator.IsRunning = true;

        var newUserFood = new CreateUserFoodDTO()
        {
            UserId = _userId,
            FoodName = FoodNameEntry.Text,
            ProteinPer100g = ConvertToDecimal(ProteinEntry.Text),
            CarbsPer100g = ConvertToDecimal(CarbsEntry.Text),
            FatPer100g = ConvertToDecimal(FatEntry.Text)
        };

        

        CreateUserFoodResponse response = await _apiAccess.CreateUserFood(newUserFood);

        string errorMessage = string.Join(Environment.NewLine, response.Details);

        Indicator.IsRunning = false;
        
        
        if (!response.isSuccess)
        {
            await DisplayAlert("Błąd", errorMessage, "OK");
        }
        else if(response.isSuccess)
        {
            await DisplayAlert("Sukces", "Utworzono.", "OK");
        }

        IsProcessing = false;
    }
    
    private decimal ConvertToDecimal(string text)
    {
        if (decimal.TryParse(text, out decimal result))
        {
            return result;
        }
        else
        {
            
            return 0;
        }
    }
    
    
    
    
}