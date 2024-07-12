using BusinessObjects;

namespace Repositories
{
    public interface IDrinkVariationRepository
    {
        List<DrinkVariation> GetVariationsByDrinkId(int drinkId);
        List<DrinkVariation> GetDrinksInPriceRange(int? minPrice, int? maxPrice);
        List<DrinkVariation> GetDrinksInCreatedDate(DateTime? startTime, DateTime? endTime);
    }
}
