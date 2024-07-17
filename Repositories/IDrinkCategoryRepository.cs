using BusinessObjects;

namespace Repositories
{
    public interface IDrinkCategoryRepository
    {
        List<DrinkCategory> GetDrinkCategories();
    }
}
