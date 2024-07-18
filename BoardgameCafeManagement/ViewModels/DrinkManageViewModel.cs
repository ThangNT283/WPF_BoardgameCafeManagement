using BoardgameCafeManagement.Commands;
using BoardgameCafeManagement.Views;
using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace BoardgameCafeManagement.ViewModels
{
    public class DrinkManageViewModel : ViewModelBase
    {
        private readonly IDrinkService _drinkService;
        private readonly DrinkManageView _drinkManageView;

        #region Fields
        private ObservableCollection<Drink> _drinks;
        public ObservableCollection<Drink> Drinks
        {
            get { return _drinks; }
            set { _drinks = value; OnPropertyChanged(nameof(Drinks)); }
        }
        private Drink _selectedDrink;
        public Drink SelectedDrink
        {
            get { return _selectedDrink; }
            set { _selectedDrink = value; OnPropertyChanged(nameof(SelectedDrink)); }
        }
        private string _searchInput;
        public string SearchInput
        {
            get { return _searchInput; }
            set { _searchInput = value; OnPropertyChanged(nameof(SearchInput)); }
        }
        #endregion

        #region Commands
        public RelayCommand RefreshList { get; }
        public RelayCommand CreateDrink { get; }
        public RelayCommand UpdateDrink { get; }
        public RelayCommand SearchDrink { get; }
        #endregion

        public DrinkManageViewModel(DrinkManageView drinkManageView)
        {
            _drinkService = new DrinkService();
            _drinkManageView = drinkManageView;

            Refresh();

            RefreshList = new RelayCommand(Refresh);
            CreateDrink = new RelayCommand(Create);
            UpdateDrink = new RelayCommand(Update);
            SearchDrink = new RelayCommand(Search);
        }

        #region Actions
        private void Refresh()
        {
            Drinks = _drinkService.GetDrinks();
            SearchInput = string.Empty;
        }
        private void Create()
        {
            new DrinkManageForm(_drinkManageView, "create", null).Show();
        }
        private void Update()
        {
            if (SelectedDrink == null)
            {
                MessageBox.Show("Please choose a drink to edit");
                return;
            }
            new DrinkManageForm(_drinkManageView, "update", SelectedDrink).Show();
        }
        private void Search()
        {
            if (SearchInput.IsNullOrEmpty())
            {
                Refresh();
                return;
            }

            SearchInput = Regex.Replace(SearchInput.Trim(), @"\s+", " ");
            Drinks = _drinkService.SearchDrink(SearchInput);
        }
        #endregion
    }
}
