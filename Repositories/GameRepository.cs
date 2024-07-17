using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class GameRepository : IGameRepository
    {
        public List<Game> GetGames() => GameDAO.GetGames();
        public bool CreateGame(Game game) => GameDAO.CreateGame(game);
        public bool UpdateGame(Game game) => GameDAO.UpdateGame(game);
        public bool DeleteGame(int id) => GameDAO.DeleteGame(id);
        public List<Game> SearchGameByName(string input) => GameDAO.SearchGameByName(input);
        public List<Game> SearchGameByType(int typeId) => GameDAO.SearchGameByType(typeId);
        public List<Game> SearchGameByRangePlayerNumber(int? min, int? max)
            => GameDAO.SearchGameByRangePlayerNumber(min, max);
        public List<Game> SearchGameByPlayerNumber(int? playerNumber)
            => GameDAO.SearchGameByPlayerNumber(playerNumber);
    }
}
