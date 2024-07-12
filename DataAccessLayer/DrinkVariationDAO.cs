using BusinessObjects;

namespace DataAccessLayer
{
    public class DrinkVariationDAO
    {
        private static readonly BoardgameCafeContext _context = new BoardgameCafeContext();

        public DrinkVariationDAO() { }

        public static List<DrinkVariation> GetVariationsByDrinkId(int drinkId)
        {
            return _context.DrinkVariations.Where(v => v.DrinkId == drinkId).ToList();
        }

        public static List<DrinkVariation> GetDrinksInPriceRange(int? minPrice, int? maxPrice)
        {
            return _context.DrinkVariations
                .Where(v => !minPrice.HasValue || v.Price >= minPrice)
                .Where(v => !maxPrice.HasValue || v.Price <= maxPrice)
                .ToList();
        }

        public static List<DrinkVariation> GetDrinksInCreatedDate(DateTime? startTime, DateTime? endTime)
        {
            return _context.DrinkVariations
                .Where(v => !startTime.HasValue || v.CreatedAt >= startTime)
                .Where(v => !endTime.HasValue || v.CreatedAt <= endTime)
                .ToList();
        }
    }
}
