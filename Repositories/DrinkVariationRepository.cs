using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class DrinkVariationRepository : IDrinkVariationRepository
    {
        public bool CreateVariation(DrinkVariation variation) => DrinkVariationDAO.CreateVariation(variation);
        public bool UpdateVariation(DrinkVariation variation) => DrinkVariationDAO.UpdateVariation(variation);
        public bool DeleteVariation(int id) => DrinkVariationDAO.DeleteVariation(id);
        public DrinkVariation GetVariationById(int id) => DrinkVariationDAO.GetVariationById(id);
        public List<DrinkVariation> GetVariationsByDrinkId(int drinkId)
            => DrinkVariationDAO.GetVariationsByDrinkId(drinkId);
        public List<DrinkVariation> GetDrinksInPriceRange(int? minPrice, int? maxPrice)
            => DrinkVariationDAO.GetDrinksInPriceRange(minPrice, maxPrice);
        public List<DrinkVariation> GetDrinksInCreatedDate(DateTime? startTime, DateTime? endTime)
            => DrinkVariationDAO.GetDrinksInCreatedDate(startTime, endTime);
        public int GetPriceByVariation(int drinkId, string variation) => DrinkVariationDAO.GetPriceByVariation(drinkId, variation);
    }
}
