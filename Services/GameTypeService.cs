using BusinessObjects;
using Repositories;
using System.Collections.ObjectModel;

namespace Services
{
    public class GameTypeService : IGameTypeService
    {
        private readonly IGameTypeRepository _gameTypeRepo;

        public GameTypeService()
        {
            _gameTypeRepo = new GameTypeRepository();
        }

        public ObservableCollection<GameType> GetGameTypes()
        {
            return new ObservableCollection<GameType>(_gameTypeRepo.GetGameTypes());
        }
        public bool CreateGameType(GameType gameType) => _gameTypeRepo.CreateGameType(gameType);
        public bool UpdateGameType(GameType gameType) => _gameTypeRepo.UpdateGameType(gameType);
        public bool DeleteGameType(int typeId) => _gameTypeRepo.DeleteGameType(typeId);
        public ObservableCollection<GameType> SearchGameType(string input)
        {
            return new ObservableCollection<GameType>(_gameTypeRepo.SearchGameType(input));
        }
    }
}
