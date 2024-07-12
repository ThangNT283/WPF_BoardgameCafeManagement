using BusinessObjects;
using System.Windows;

namespace DataAccessLayer
{
    public class DrinkDAO
    {
        private static readonly BoardgameCafeContext _context = new BoardgameCafeContext();

        public DrinkDAO() { }

        public static List<Drink> GetDrinks()
        {
            return _context.Drinks.ToList();
        }

        public static bool CreateDrink(Drink drink)
        {
            try
            {
                _context.Drinks.Add(drink);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool UpdateDrink(Drink drink)
        {
            try
            {
                Drink? drinkToUpdate = _context.Drinks.Find(drink.Id);
                if (drinkToUpdate == null)
                {
                    throw new Exception("Drink ID " + drink.Id + " not found!");
                }
                _context.Entry(drinkToUpdate).CurrentValues.SetValues(drink);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
