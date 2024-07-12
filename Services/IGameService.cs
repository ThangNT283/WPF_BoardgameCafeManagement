using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IGameService
    {
        ObservableCollection<Game> GetGames();
        bool CreateGame(Game game);
        bool UpdateGame(Game game);
        bool DeleteGame(int typeId);
        ObservableCollection<Game> SearchGameByName(string input);
        ObservableCollection<Game> SearchGameByType(int typeId);
        ObservableCollection<Game> SearchGameByRangePlayerNumber(int? min, int? max);
        ObservableCollection<Game> SearchGameByPlayerNumber(int? playerNumber);
    }
}
