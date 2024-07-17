using BusinessObjects;
using Repositories;

namespace Services
{
    public class DrinkCategoryService : IDrinkCategoryService
    {
        private readonly IDrinkCategoryRepository _drinkCategoryRepository;

        public DrinkCategoryService()
        {
            _drinkCategoryRepository = new DrinkCategoryRepository();
        }

        public List<DrinkCategory> GetDrinkCategories() => _drinkCategoryRepository.GetDrinkCategories();
    }
}
