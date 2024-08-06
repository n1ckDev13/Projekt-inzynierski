using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ApiServiceAccess;
using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.DTOs.UserFoodDTOs;
using ClassLibrary.Responses.UserFood;

namespace Diet;

public partial class UserFoodPage : ContentPage
{
    private ObservableCollection<GetAllUserFoodDTO> _userFood = new();
    
    public ObservableCollection<GetAllUserFoodDTO> UserFoods
    {
        get => _userFood;
        set
        {
            if(_userFood == value) return;

            _userFood = value;
            OnPropertyChanged();
        }
    }
    
    private ObservableCollection<GetAllUserFoodDTO> _filteredUserFood = new();
    
    public ObservableCollection<GetAllUserFoodDTO> FilteredUserFoods
    {
        get => _filteredUserFood;
        set
        {
            if(_filteredUserFood == value) return;

            _filteredUserFood = value;
            OnPropertyChanged();
        }
    }

    private UserCredentialsDTO _userCredentials;
    
    public UserCredentialsDTO UserCredentials
    {
        get => _userCredentials;
        set
        {
            if(_userCredentials == value) return;

            _userCredentials = value;
            OnPropertyChanged();
        }
    }
    
    private string _filterText;
    
    public string FilterText
    {
        get => _filterText;
        set
        {
            if(_filterText == value) return;

            _filterText = value;
            OnPropertyChanged();
            ApplyFilter();
        }
    }
    
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
    
    public ICommand RefreshingCommand { get; }
    private readonly IApiAccess _apiAccess;
    
    public UserFoodPage(IApiAccess apiAccess)
    {
        _apiAccess = apiAccess;
        
        InitializeComponent();

        UserCredentials = _apiAccess.GetUserCredentials();
        
        RefreshingCommand = new Command(() => LoadPageContent());

        BindingContext = this;
        
        LoadPageContent();
    }
    
    private async void LoadPageContent()
    {
        UserFoods.Clear();
        
        IsPageLoading = true;

        try
        {
            List<GetAllUserFoodDTO> response = await _apiAccess.GetAllUserFoods(UserCredentials.Id);
            
            if (response == null)
            {
                response = new List<GetAllUserFoodDTO>();
            }

            UserFoods = new ObservableCollection<GetAllUserFoodDTO>(response);
            FilteredUserFoods = UserFoods;

            BindingContext = this;
        }
        finally
        {
            IsPageLoading = false;
        }
        
        IsPageLoading = false;
    }
    
    private void ApplyFilter()
    {
        if (string.IsNullOrWhiteSpace(_filterText))
        {
            FilteredUserFoods = UserFoods;
        }
        else
        {
            var lowerCaseFiler = _filterText.ToLower();
            FilteredUserFoods = new ObservableCollection<GetAllUserFoodDTO>(UserFoods.Where(food => food.FoodName.ToLower().Contains(lowerCaseFiler)));
        }
    }
    
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        FilterText = e.NewTextValue;
    }

    private void OnAddButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddUserFoodPage(_apiAccess, _userCredentials.Id));
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        
        LoadPageContent();
    }
    
    private void OnEditButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is GetAllUserFoodDTO userFoodToEdit)
        {
            Navigation.PushAsync(new EditUserFoodPage(_apiAccess, userFoodToEdit));
        }
    }

    private async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is GetAllUserFoodDTO userFoodToDelete)
        {
            bool isUserSure = await DisplayAlert("Potwierdzenie", "Czy na pewno usunąć?", "Tak", "Nie");

            if (isUserSure)
            {
                DeleteUserFoodResponse response = await _apiAccess.DeleteUserFood(userFoodToDelete.Id);
                
                string errorMessage = string.Join(Environment.NewLine, response.Details);
        
        
                if (!response.isSuccess)
                {
                    await DisplayAlert("Błąd", errorMessage, "OK");
                }
                else if(response.isSuccess)
                {
                    await DisplayAlert("Sukces", "Usunięto.", "OK");
                    LoadPageContent();
                }
            }
        }
    }
    
    
}