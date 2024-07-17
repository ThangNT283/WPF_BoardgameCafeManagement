using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class DrinkCategoryRepository : IDrinkCategoryRepository
    {
        public List<DrinkCategory> GetDrinkCategories() => DrinkCategoryDAO.GetDrinkCategories();
    }
}
