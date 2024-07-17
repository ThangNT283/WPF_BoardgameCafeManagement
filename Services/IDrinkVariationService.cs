using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IDrinkVariationService
    {
        bool CreateVariation(DrinkVariation variation);
        bool UpdateVariation(DrinkVariation variation);
        bool DeleteVariation(int id);
        ObservableCollection<DrinkVariation> GetVariationsByDrinkId(int drinkId);
        ObservableCollection<DrinkVariation> GetDrinksInPriceRange(int? minPrice, int? maxPrice);
        ObservableCollection<DrinkVariation> GetDrinksInCreatedDate(DateTime? startTime, DateTime? endTime);
        int GetPriceByVariation(int drinkId, string variation);
    }
}
