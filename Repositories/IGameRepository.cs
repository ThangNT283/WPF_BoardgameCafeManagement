using BusinessObjects;

namespace Repositories
{
    public interface IGameRepository
    {
        List<Game> GetGames();
        bool CreateGame(Game game);
        bool UpdateGame(Game game);
        bool DeleteGame(int id);
        List<Game> SearchGameByName(string input);
        List<Game> SearchGameByType(int typeId);
        List<Game> SearchGameByRangePlayerNumber(int? min, int? max);
        List<Game> SearchGameByPlayerNumber(int? playerNumber);
    }
}
