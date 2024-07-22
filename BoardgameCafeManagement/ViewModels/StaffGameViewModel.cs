using BoardgameCafeManagement.Commands;
using BusinessObjects;
using BusinessObjects.Builders;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace BoardgameCafeManagement.ViewModels
{
    public class StaffGameViewModel : ViewModelBase
    {
        private readonly IGameService _gameService;

        private static string SPECIAL_CHAR_PATTERN = @"[!@#$%^&*(),.?""':{}|<>]";
        private static string LETTER_PATTERN = @"[a-zA-Z]";
        private static string SPACE_PATTERN = @"\s";

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
        private string _playerNumber;
        public string PlayerNumber
        {
            get { return _playerNumber; }
            set { _playerNumber = value; OnPropertyChanged(nameof(PlayerNumber)); }
        }
        private string _playerNumberError;
        public string PlayerNumberError
        {
            get { return _playerNumberError; }
            set { _playerNumberError = value; OnPropertyChanged(nameof(PlayerNumberError)); }
        }
        #endregion

        #region Commands
        public RelayCommand GameSelectionChanged { get; }
        public RelayCommand UpdateGame { get; }
        public RelayCommand SearchGame { get; }
        #endregion

        public StaffGameViewModel()
        {
            _gameService = new GameService();

            Games = _gameService.GetGames();

            GameSelectionChanged = new RelayCommand(LoadDataFields);
            UpdateGame = new RelayCommand(UpdateStatus);
            SearchGame = new RelayCommand(Search);
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

        private void Search()
        {
            if (SearchInput != null && PlayerNumber != null)
            {
                SearchInput = Regex.Replace(SearchInput.Trim(), @"\s+", " ");
                PlayerNumber = PlayerNumber.Trim();
            }

            Games = _gameService.SearchGameByName(SearchInput);
            if (!PlayerNumber.IsNullOrEmpty() && IsValidPlayerNumber())
            {
                Games = new ObservableCollection<Game>(Games.
                    Where(g => g.MinPlayerNumber <= Convert.ToInt32(PlayerNumber)
                        && g.MaxPlayerNumber >= Convert.ToInt32(PlayerNumber)));
            }
        }

        private void Refresh()
        {
            SelectedGameName = "";
            Status = false;
            SelectedGame = null;

            Search();
        }
        #endregion

        #region Validation
        private bool IsValidPlayerNumber()
        {
            PlayerNumber = _playerNumber.Trim();
            if (Regex.IsMatch(_playerNumber, SPECIAL_CHAR_PATTERN) ||
                Regex.IsMatch(_playerNumber, SPACE_PATTERN) ||
                Regex.IsMatch(_playerNumber, LETTER_PATTERN))
            {
                PlayerNumberError = "Invalid";
                return false;
            }

            try
            {
                if (!_playerNumber.IsNullOrEmpty() && Convert.ToInt32(_playerNumber) <= 0)
                {
                    PlayerNumberError = "Invalid";
                    return false;
                }
            }
            catch (Exception ex)
            {
                PlayerNumberError = "Invalid";
                MessageBox.Show(ex.Message);
                return false;
            }

            PlayerNumberError = string.Empty;
            return true;
        }
        #endregion
    }
}
