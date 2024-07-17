namespace BusinessObjects.Builders
{
    public interface IDrinkVariationBuilder
    {
        IDrinkVariationBuilder SetId(int id);
        IDrinkVariationBuilder SetDrinkId(int drinkId);
        IDrinkVariationBuilder SetName(string name);
        IDrinkVariationBuilder SetPrice(int price);
        IDrinkVariationBuilder SetCreatedAt(DateTime createdAt);
        IDrinkVariationBuilder SetStatus(bool status);
        DrinkVariation Build();
    }
}
