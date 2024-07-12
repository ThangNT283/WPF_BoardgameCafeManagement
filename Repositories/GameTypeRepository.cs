using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class GameTypeRepository : IGameTypeRepository
    {
        public List<GameType> GetGameTypes() => GameTypeDAO.GetGameTypes();
        public bool CreateGameType(GameType gameType) => GameTypeDAO.CreateGameType(gameType);
        public bool UpdateGameType(GameType gameType) => GameTypeDAO.UpdateGameType(gameType);
        public bool DeleteGameType(int typeId) => GameTypeDAO.DeleteGameType(typeId);
        public List<GameType> SearchGameType(string input) => GameTypeDAO.SearchGameType(input);
    }
}
