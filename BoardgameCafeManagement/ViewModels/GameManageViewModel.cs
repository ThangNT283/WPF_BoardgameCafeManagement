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
    public class GameManageViewModel : ViewModelBase
    {
        private readonly IGameService _gameService;
        private readonly GameManageView _gameManageView;

        #region Fields
        private ObservableCollection<Game> _games;
        public ObservableCollection<Game> Games
        {
            get { return _games; }
            set { _games = value; OnPropertyChanged(nameof(Games)); }
        }
        private Game _selectedGame;
        public Game SelectedGame
        {
            get { return _selectedGame; }
            set { _selectedGame = value; OnPropertyChanged(nameof(SelectedGame)); }
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
        public RelayCommand CreateGame { get; }
        public RelayCommand UpdateGame { get; }
        public RelayCommand DeleteGame { get; }
        public RelayCommand SearchGame { get; }
        #endregion

        public GameManageViewModel(GameManageView gameManageView)
        {
            _gameService = new GameService();
            _gameManageView = gameManageView;

            Refresh();

            RefreshList = new RelayCommand(Refresh);
            CreateGame = new RelayCommand(Create);
            UpdateGame = new RelayCommand(Update);
            DeleteGame = new RelayCommand(Delete);
            SearchGame = new RelayCommand(Search);
        }

        #region Actions
        private void Refresh()
        {
            Games = _gameService.GetGames();
            SearchInput = string.Empty;
        }
        private void Create()
        {
            new GameManageForm(_gameManageView, "create", null).Show();
        }
        private void Update()
        {
            if (SelectedGame == null)
            {
                MessageBox.Show("Please choose a game to edit");
                return;
            }
            new GameManageForm(_gameManageView, "update", SelectedGame).Show();
        }

        private void Delete()
        {
            if (SelectedGame == null)
            {
                MessageBox.Show("Please choose a game to delete");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do you want to delete " + SelectedGame.Name + "?",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                bool isSuccess = _gameService.DeleteGame(SelectedGame.Id);
                if (isSuccess)
                {
                    MessageBox.Show("Delete game successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to delete game");
                }

                Refresh();
            }
        }

        private void Search()
        {
            if (SearchInput.IsNullOrEmpty())
            {
                Refresh();
                return;
            }

            SearchInput = Regex.Replace(SearchInput.Trim(), @"\s+", " ");
            Games = _gameService.SearchGameByName(SearchInput);
        }
        #endregion
    }
}
