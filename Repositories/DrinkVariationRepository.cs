using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class DrinkVariationRepository : IDrinkVariationRepository
    {
        public List<DrinkVariation> GetVariationsByDrinkId(int drinkId)
            => DrinkVariationDAO.GetVariationsByDrinkId(drinkId);
        public List<DrinkVariation> GetDrinksInPriceRange(int? minPrice, int? maxPrice)
            => DrinkVariationDAO.GetDrinksInPriceRange(minPrice, maxPrice);
        public List<DrinkVariation> GetDrinksInCreatedDate(DateTime? startTime, DateTime? endTime)
            => DrinkVariationDAO.GetDrinksInCreatedDate(startTime, endTime);
    }
}
