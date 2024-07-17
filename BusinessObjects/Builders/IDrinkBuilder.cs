namespace BusinessObjects.Builders
{
    public interface IDrinkBuilder
    {
        IDrinkBuilder SetId(int id);
        IDrinkBuilder SetName(string name);
        IDrinkBuilder SetCategoryId(int categoryId);
        IDrinkBuilder SetStatus(bool status);
        IDrinkBuilder SetCategory(DrinkCategory category);
        Drink Build();
    }
}
