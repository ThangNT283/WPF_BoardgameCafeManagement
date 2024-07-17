namespace BusinessObjects.Builders
{
    public interface IGameBuilder
    {
        IGameBuilder SetId(int id);
        IGameBuilder SetName(string name);
        IGameBuilder SetCreatedAt(DateTime createdAt);
        IGameBuilder SetMinPlayer(int minPlayer);
        IGameBuilder SetMaxPlayer(int maxPlayer);
        IGameBuilder SetTypeId(int typeId);
        IGameBuilder SetStatus(bool status);
        Game Build();
    }
}
