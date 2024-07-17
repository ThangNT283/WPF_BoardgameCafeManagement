using BoardgameCafeManagement.Commands;
using BusinessObjects;
using BusinessObjects.Builders;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace BoardgameCafeManagement.ViewModels
{
    public class StaffGameViewModel : ViewModelBase
    {
        private readonly IGameService _gameService;

        #region Fields
        private ObservableCollection<Game> _games;
        public ObservableCollection<Game> Games
        {
            get { return _games; }
            set { _games = value; OnPropertyChanged(nameof(Games)); }
        }
        private Game? _selectedGame;
        public Game? SelectedGame
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
        private bool _status;
        public bool Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }
        private string? _selectedGameName;
        public string? SelectedGameName
        {
            get { return _selectedGameName; }
            set { _selectedGameName = value; OnPropertyChanged(nameof(SelectedGameName)); }
        }
        #endregion

        #region Commands
        public RelayCommand GameSelectionChanged { get; }
        public RelayCommand UpdateGame { get; }
        #endregion

        public StaffGameViewModel()
        {
            _gameService = new GameService();

            Games = _gameService.GetGames();

            GameSelectionChanged = new RelayCommand(LoadDataFields);
            UpdateGame = new RelayCommand(UpdateStatus);
        }

        #region Actions
        private void LoadDataFields()
        {
            if (SelectedGame != null)
            {
                SelectedGameName = SelectedGame.Name;
                Status = SelectedGame.Status;
            }
        }

        private void UpdateStatus()
        {
            if (SelectedGameName.IsNullOrEmpty())
            {
                MessageBox.Show("Please choose a game to update");
                return;
            }

            IGameBuilder gameBuilder = new GameBuilder();
            Game game = gameBuilder
                .SetId(SelectedGame.Id)
                .SetName(SelectedGameName)
                .SetCreatedAt(SelectedGame.CreatedAt)
                .SetMinPlayer(SelectedGame.MinPlayerNumber)
                .SetMaxPlayer(SelectedGame.MaxPlayerNumber)
                .SetTypeId(SelectedGame.TypeId)
                .SetStatus(Status)
                .Build();
            bool isSuccess = _gameService.UpdateGame(game);
            if (isSuccess)
            {
                MessageBox.Show("Update game successfully!");
            }
            else
            {
                MessageBox.Show("Failed to update game");
            }

            Refresh();
        }

        private void Refresh()
        {
            SelectedGameName = "";
            Status = false;
            SelectedGame = null;

            Games = _gameService.GetGames();
        }
        #endregion
    }
}
