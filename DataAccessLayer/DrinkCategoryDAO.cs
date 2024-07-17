using BusinessObjects;

namespace DataAccessLayer
{
    public class DrinkCategoryDAO
    {
        private static readonly BoardgameCafeContext _context = new BoardgameCafeContext();

        public DrinkCategoryDAO() { }

        public static List<DrinkCategory> GetDrinkCategories()
        {
            return _context.DrinkCategories.ToList();
        }
    }
}
