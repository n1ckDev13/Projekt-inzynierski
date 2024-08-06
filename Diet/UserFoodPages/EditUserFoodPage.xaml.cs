using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiServiceAccess;
using ClassLibrary.DTOs.UserFoodDTOs;
using ClassLibrary.Responses.UserFood;

namespace Diet;

public partial class EditUserFoodPage : ContentPage
{
    private readonly IApiAccess _apiAccess;
    private GetAllUserFoodDTO _userFoodToEdit;
    
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
    
    public EditUserFoodPage(IApiAccess apiAccess, GetAllUserFoodDTO userFoodToEdit)
    {
        _apiAccess = apiAccess;
        _userFoodToEdit = userFoodToEdit;
        InitializeComponent();
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        FoodNameEntry.Text = _userFoodToEdit.FoodName;
        ProteinEntry.Text = _userFoodToEdit.ProteinPer100g.ToString();
        CarbsEntry.Text = _userFoodToEdit.CarbsPer100g.ToString();
        FatEntry.Text = _userFoodToEdit.FatPer100g.ToString();

    }
    
    private async void OnEditButtonClicked(object sender, EventArgs e)
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

        var updateUserFood = new UpdateUserFoodDTO()
        {
            Id = _userFoodToEdit.Id,
            FoodName = FoodNameEntry.Text,
            ProteinPer100g = ConvertToDecimal(ProteinEntry.Text),
            CarbsPer100g = ConvertToDecimal(CarbsEntry.Text),
            FatPer100g = ConvertToDecimal(FatEntry.Text)
        };

        

        UpdateUserFoodResponse response = await _apiAccess.UpdateUserFood(updateUserFood);

        string errorMessage = string.Join(Environment.NewLine, response.Details);

        Indicator.IsRunning = false;
        
        
        if (!response.isSuccess)
        {
            await DisplayAlert("Błąd", errorMessage, "OK");
        }
        else if(response.isSuccess)
        {
            await DisplayAlert("Sukces", "Edytowano.", "OK");
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