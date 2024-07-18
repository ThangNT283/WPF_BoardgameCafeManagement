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

        public bool CreateVariation(DrinkVariation variation) => _drinkVariationRepo.CreateVariation(variation);
        public bool UpdateVariation(DrinkVariation variation) => _drinkVariationRepo.UpdateVariation(variation);
        public bool DeleteVariation(int id) => _drinkVariationRepo.DeleteVariation(id);
        public DrinkVariation GetVariationById(int id) => _drinkVariationRepo.GetVariationById(id);
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
        public int GetPriceByVariation(int drinkId, string variation) => _drinkVariationRepo.GetPriceByVariation(drinkId, variation);
    }
}
