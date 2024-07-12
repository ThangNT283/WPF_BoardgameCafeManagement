using BusinessObjects;
using Repositories;
using System.Collections.ObjectModel;

namespace Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepo;

        public GameService()
        {
            _gameRepo = new GameRepository();
        }

        public ObservableCollection<Game> GetGames()
        {
            return new ObservableCollection<Game>(_gameRepo.GetGames());
        }
        public bool CreateGame(Game game) => _gameRepo.CreateGame(game);
        public bool UpdateGame(Game game) => _gameRepo.UpdateGame(game);
        public bool DeleteGame(int typeId) => _gameRepo.DeleteGame(typeId);
        public ObservableCollection<Game> SearchGameByName(string input)
        {
            return new ObservableCollection<Game>(_gameRepo.SearchGameByName(input));
        }
        public ObservableCollection<Game> SearchGameByType(int typeId)
        {
            return new ObservableCollection<Game>(_gameRepo.SearchGameByType(typeId));
        }
        public ObservableCollection<Game> SearchGameByRangePlayerNumber(int? min, int? max)
        {
            return new ObservableCollection<Game>(_gameRepo.SearchGameByRangePlayerNumber(min, max));
        }
        public ObservableCollection<Game> SearchGameByPlayerNumber(int? playerNumber)
        {
            return new ObservableCollection<Game>(_gameRepo.SearchGameByPlayerNumber(playerNumber));
        }
    }
}
