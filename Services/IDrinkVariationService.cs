using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IDrinkVariationService
    {
        ObservableCollection<DrinkVariation> GetVariationsByDrinkId(int drinkId);
        ObservableCollection<DrinkVariation> GetDrinksInPriceRange(int? minPrice, int? maxPrice);
        ObservableCollection<DrinkVariation> GetDrinksInCreatedDate(DateTime? startTime, DateTime? endTime);
    }
}
