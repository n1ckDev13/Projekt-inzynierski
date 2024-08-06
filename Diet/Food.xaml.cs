using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ApiServiceAccess;
using ClassLibrary.DTOs.FoodDTOs;
using ClassLibrary.Responses.Food;

namespace Diet;

public partial class Food : ContentPage
{
    private ObservableCollection<GetAllFoodsDTO> _foods = new();
    

    public ObservableCollection<GetAllFoodsDTO> Foods
    {
        get => _foods;
        set
        {
            if(_foods == value) return;

            _foods = value;
            OnPropertyChanged();
        }
    }
    
    private ObservableCollection<GetAllFoodsDTO> _filteredFoods = new();
    
    
    public ObservableCollection<GetAllFoodsDTO> FilteredFoods
    {
        get => _filteredFoods;
        set
        {
            if(_filteredFoods == value) return;

            _filteredFoods = value;
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
    
    public Food(IApiAccess apiAccess)
    {
        
        
        InitializeComponent();

        _apiAccess = apiAccess;

        RefreshingCommand = new Command(() => LoadPageContent());

        BindingContext = this;
        
        LoadPageContent();
    }

    private async void LoadPageContent()
    {
        Foods.Clear();
        
        IsPageLoading = true;

        try
        {
            List<GetAllFoodsDTO> response = await _apiAccess.GetAllFoods();

            if (response == null)
            {
                response = new List<GetAllFoodsDTO>();
            }

            Foods = new ObservableCollection<GetAllFoodsDTO>(response);
            FilteredFoods = Foods;

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
            FilteredFoods = Foods;
        }
        else
        {
            var lowerCaseFiler = _filterText.ToLower();
            FilteredFoods = new ObservableCollection<GetAllFoodsDTO>(Foods.Where(food => food.FoodName.ToLower().Contains(lowerCaseFiler)));
        }
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        FilterText = e.NewTextValue;
    }
}