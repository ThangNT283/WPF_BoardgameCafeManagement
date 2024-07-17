namespace BusinessObjects.Builders
{
    public interface IBillBuilder
    {
        IBillBuilder SetId(int id);
        IBillBuilder SetTableId(int tableId);
        IBillBuilder SetCustomerName(string customerName);
        IBillBuilder SetNumberOfGames(int numberOfGames);
        Bill Build();
    }
}
