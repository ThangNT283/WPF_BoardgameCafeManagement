using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IGameTypeService
    {
        ObservableCollection<GameType> GetGameTypes();
        bool CreateGameType(GameType gameType);
        bool UpdateGameType(GameType gameType);
        bool DeleteGameType(int typeId);
        ObservableCollection<GameType> SearchGameType(string input);
    }
}
