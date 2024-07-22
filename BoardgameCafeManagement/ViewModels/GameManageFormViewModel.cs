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
    public class GameManageFormViewModel : ViewModelBase
    {
        private readonly IGameService _gameService;
        private readonly IGameTypeService _gameTypeService;
        private readonly Game? _selectedGame;

        private static string SPECIAL_CHAR_PATTERN = @"[!@#$%^&*(),.?""':{}|<>]";
        private static string NUMBER_PATTERN = @"[0-9]";
        private static string LETTER_PATTERN = @"[a-zA-Z]";
        private static string SPACE_PATTERN = @"\s";

        #region Properties
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        private string _minPlayer;
        public string MinPlayer
        {
            get { return _minPlayer; }
            set { _minPlayer = value; OnPropertyChanged(nameof(MinPlayer)); }
        }
        private string _maxPlayer;
        public string MaxPlayer
        {
            get { return _maxPlayer; }
            set { _maxPlayer = value; OnPropertyChanged(nameof(MaxPlayer)); }
        }
        private ObservableCollection<GameType> _gameTypes;
        public ObservableCollection<GameType> GameTypes
        {
            get { return _gameTypes; }
            set { _gameTypes = value; OnPropertyChanged(nameof(GameTypes)); }
        }
        private int _gameTypeId;
        public int GameTypeId
        {
            get { return _gameTypeId; }
            set { _gameTypeId = value; OnPropertyChanged(nameof(GameTypeId)); }
        }
        private bool _status;
        public bool Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }
        private string _actionName;
        public string ActionName
        {
            get { return _actionName; }
            set { _actionName = value; OnPropertyChanged(nameof(ActionName)); }
        }
        #endregion

        #region Errors
        private string _nameError;
        public string NameError
        {
            get { return _nameError; }
            set { _nameError = value; OnPropertyChanged(nameof(NameError)); }
        }
        private string _minPlayerError;
        public string MinPlayerError
        {
            get { return _minPlayerError; }
            set { _minPlayerError = value; OnPropertyChanged(nameof(MinPlayerError)); }
        }
        private string _maxPlayerError;
        public string MaxPlayerError
        {
            get { return _maxPlayerError; }
            set { _maxPlayerError = value; OnPropertyChanged(nameof(MaxPlayerError)); }
        }
        private string _gameTypeError;
        public string GameTypeError
        {
            get { return _gameTypeError; }
            set { _gameTypeError = value; OnPropertyChanged(nameof(GameTypeError)); }
        }
        #endregion

        #region Commands
        public RelayCommand Action { get; }
        #endregion

        public GameManageFormViewModel(string action, Game? selectedGame)
        {
            _gameService = new GameService();
            _gameTypeService = new GameTypeService();
            ActionName = action.ToUpper();
            GameTypes = _gameTypeService.GetGameTypes();

            _selectedGame = selectedGame;
            if (_selectedGame != null)
            {
                Id = _selectedGame.Id;
                Name = _selectedGame.Name;
                MinPlayer = _selectedGame.MinPlayerNumber.ToString();
                MaxPlayer = _selectedGame.MaxPlayerNumber.ToString();
                GameTypeId = _selectedGame.TypeId;
                Status = _selectedGame.Status;
            }

            if (action.Equals("create", StringComparison.OrdinalIgnoreCase))
            {
                Action = new RelayCommand(CreateGame);
            }
            else if (action.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                Action = new RelayCommand(UpdateGame);
            }
        }

        #region Actions
        private void CreateGame()
        {
            try
            {
                if (IsValidFields())
                {
                    IGameBuilder gameBuilder = new GameBuilder();
                    Game game = gameBuilder.SetName(Name)
                        .SetMinPlayer(Convert.ToInt32(MinPlayer))
                        .SetMaxPlayer(Convert.ToInt32(MaxPlayer))
                        .SetTypeId(GameTypeId)
                        .SetStatus(Status)
                        .Build();
                    bool isSuccess = _gameService.CreateGame(game);

                    if (!isSuccess)
                    {
                        MessageBox.Show("Failed to create game!");
                    }
                    else
                    {
                        MessageBox.Show("Create game successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateGame()
        {
            try
            {
                if (IsValidFields())
                {
                    IGameBuilder gameBuilder = new GameBuilder();
                    Game game = gameBuilder.SetId(_selectedGame.Id)
                        .SetName(Name)
                        .SetCreatedAt(_selectedGame.CreatedAt)
                        .SetMinPlayer(Convert.ToInt32(MinPlayer))
                        .SetMaxPlayer(Convert.ToInt32(MaxPlayer))
                        .SetTypeId(GameTypeId)
                        .SetStatus(Status)
                        .Build();
                    bool isSuccess = _gameService.UpdateGame(game);

                    if (!isSuccess)
                    {
                        MessageBox.Show("Failed to update game!");
                    }
                    else
                    {
                        MessageBox.Show("Update game successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Validating
        private bool IsValidFields()
        {
            bool isValid = true;

            isValid &= ValidateName();
            isValid &= IsMinNumberValid();
            isValid &= IsMaxNumberValid();
            isValid &= IsValidGameType();

            return isValid;
        }

        private bool IsValidGameType()
        {
            if (GameTypeId <= 0)
            {
                GameTypeError = "Game type cannot be empty";
                return false;
            }

            GameTypeError = "";
            return true;
        }

        private bool ValidateName()
        {
            if (_name.IsNullOrEmpty())
            {
                NameError = "Game name cannot be empty";
                return false;
            }

            Name = Regex.Replace(_name.Trim(), @"\s+", " ");
            if (Regex.IsMatch(_name, SPECIAL_CHAR_PATTERN) || Regex.IsMatch(_name, NUMBER_PATTERN))
            {
                NameError = "No special character or number in game name";
                return false;
            }
            if (_name.Length > 128)
            {
                NameError = "Game name is too long";
                return false;
            }

            NameError = string.Empty;
            return true;
        }

        private bool IsMinNumberValid()
        {
            if (MinPlayer.IsNullOrEmpty())
            {
                MinPlayerError = "Minimum player number cannot be empty";
                return false;
            }

            MinPlayer = _minPlayer.Trim();
            if (Regex.IsMatch(_minPlayer, SPECIAL_CHAR_PATTERN) ||
                Regex.IsMatch(_minPlayer, SPACE_PATTERN) ||
                Regex.IsMatch(_minPlayer, LETTER_PATTERN))
            {
                MinPlayerError = "Minimum player number can only be number";
                return false;
            }

            if (_minPlayer.IsNullOrEmpty()) { return true; }

            try
            {
                if (Convert.ToInt32(_minPlayer) <= 0)
                {
                    MinPlayerError = "Minimum player number must be larger than 0";
                    return false;
                }
            }
            catch (Exception ex)
            {
                MinPlayerError = "Invalid minimum player number";
                MessageBox.Show(ex.Message);
                return false;
            }

            MinPlayerError = string.Empty;
            return true;
        }

        private bool IsMaxNumberValid()
        {
            if (MaxPlayer.IsNullOrEmpty())
            {
                MaxPlayerError = "Maximum player number cannot be empty";
                return false;
            }

            MaxPlayer = _maxPlayer.Trim();
            if (Regex.IsMatch(_maxPlayer, SPECIAL_CHAR_PATTERN) ||
                Regex.IsMatch(_maxPlayer, SPACE_PATTERN) ||
                Regex.IsMatch(_maxPlayer, LETTER_PATTERN))
            {
                MaxPlayerError = "Maximum player number can only be number";
                return false;
            }

            if (_maxPlayer.IsNullOrEmpty()) { return true; }

            try
            {
                if (Convert.ToInt32(_maxPlayer) <= 0)
                {
                    MaxPlayerError = "Maximum player number must be larger than 0";
                    return false;
                }
                if (Convert.ToInt32(_maxPlayer) < Convert.ToInt32(_minPlayer))
                {
                    MaxPlayerError = "Maximum player number cannot be smaller than minimum player number";
                    return false;
                }
            }
            catch (Exception ex)
            {
                MaxPlayerError = "Invalid maximum player number";
                MessageBox.Show(ex.Message);
                return false;
            }

            MaxPlayerError = string.Empty;
            return true;
        }
        #endregion
    }
}