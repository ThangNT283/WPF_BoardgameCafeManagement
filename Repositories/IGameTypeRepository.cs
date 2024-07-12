using BusinessObjects;

namespace Repositories
{
    public interface IGameTypeRepository
    {
        List<GameType> GetGameTypes();
        bool CreateGameType(GameType gameType);
        bool UpdateGameType(GameType gameType);
        bool DeleteGameType(int typeId);
        List<GameType> SearchGameType(string input);
    }
}
