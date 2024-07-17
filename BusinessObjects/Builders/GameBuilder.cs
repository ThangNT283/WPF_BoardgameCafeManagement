namespace BusinessObjects.Builders
{
    public class GameBuilder : IGameBuilder
    {
        private Game _game;

        public GameBuilder()
        {
            _game = new Game();
        }

        public IGameBuilder SetId(int id)
        {
            _game.Id = id;
            return this;
        }
        public IGameBuilder SetName(string name)
        {
            _game.Name = name;
            return this;
        }
        public IGameBuilder SetCreatedAt(DateTime createdAt)
        {
            _game.CreatedAt = createdAt;
            return this;
        }
        public IGameBuilder SetMinPlayer(int minPlayer)
        {
            _game.MinPlayerNumber = minPlayer;
            return this;
        }
        public IGameBuilder SetMaxPlayer(int maxPlayer)
        {
            _game.MaxPlayerNumber = maxPlayer;
            return this;
        }
        public IGameBuilder SetTypeId(int typeId)
        {
            _game.TypeId = typeId;
            return this;
        }
        public IGameBuilder SetStatus(bool status)
        {
            _game.Status = status;
            return this;
        }
        public Game Build() => _game;
    }
}
