using BusinessObjects;

namespace Repositories
{
    public interface IDrinkVariationRepository
    {
        bool CreateVariation(DrinkVariation variation);
        bool UpdateVariation(DrinkVariation variation);
        bool DeleteVariation(int id);
        List<DrinkVariation> GetVariationsByDrinkId(int drinkId);
        List<DrinkVariation> GetDrinksInPriceRange(int? minPrice, int? maxPrice);
        List<DrinkVariation> GetDrinksInCreatedDate(DateTime? startTime, DateTime? endTime);
        int GetPriceByVariation(int drinkId, string variation);
    }
}
