using BusinessObjects;
using Repositories;
using System.Collections.ObjectModel;

namespace Services
{
    public class DrinkVariationService : IDrinkVariationService
    {
        private readonly IDrinkVariationRepository _drinkVariationRepo;

        public DrinkVariationService()
        {
            _drinkVariationRepo = new DrinkVariationRepository();
        }

        public ObservableCollection<DrinkVariation> GetVariationsByDrinkId(int drinkId)
        {
            return new ObservableCollection<DrinkVariation>(_drinkVariationRepo.GetVariationsByDrinkId(drinkId));
        }
        public ObservableCollection<DrinkVariation> GetDrinksInPriceRange(int? minPrice, int? maxPrice)
        {
            return new ObservableCollection<DrinkVariation>(_drinkVariationRepo.GetDrinksInPriceRange(minPrice, maxPrice));
        }
        public ObservableCollection<DrinkVariation> GetDrinksInCreatedDate(DateTime? startTime, DateTime? endTime)
        {
            return new ObservableCollection<DrinkVariation>(_drinkVariationRepo.GetDrinksInCreatedDate(startTime, endTime));
        }
    }
}
