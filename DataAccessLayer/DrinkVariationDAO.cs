using BusinessObjects;
using System.Windows;

namespace DataAccessLayer
{
    public class DrinkVariationDAO
    {
        private static readonly BoardgameCafeContext _context = new BoardgameCafeContext();

        public DrinkVariationDAO() { }

        public static bool CreateVariation(DrinkVariation variation)
        {
            try
            {
                MessageBox.Show(variation.VariationName);

                _context.DrinkVariations.Add(variation);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";
                var errorMessage = $"{ex.Message}\nInner Exception: {innerExceptionMessage}\n{ex.StackTrace}";
                MessageBox.Show(errorMessage);
                return false;
            }
        }

        public static bool UpdateVariation(DrinkVariation variation)
        {
            try
            {
                DrinkVariation? variationToUpdate = _context.DrinkVariations.Find(variation.Id);
                if (variationToUpdate == null)
                {
                    throw new Exception("Drink variation ID " + variation.Id + " not found!");
                }
                _context.Entry(variationToUpdate).CurrentValues.SetValues(variation);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";
                var errorMessage = $"{ex.Message}\nInner Exception: {innerExceptionMessage}\n{ex.StackTrace}";
                MessageBox.Show(errorMessage);
                return false;
            }
        }

        public static bool DeleteVariation(int id)
        {
            try
            {
                DrinkVariation? variationToDelete = _context.DrinkVariations.Find(id);
                if (variationToDelete == null)
                {
                    throw new Exception("Drink Variation ID " + id + " not found!");
                }

                _context.DrinkVariations.Remove(variationToDelete);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

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

        public static int GetPriceByVariation(int drinkId, string variation)
        {
            DrinkVariation? res = _context.DrinkVariations.FirstOrDefault(v => v.DrinkId == drinkId && v.VariationName == variation);
            return res is null ? -1 : res.Price;
        }
    }
}
