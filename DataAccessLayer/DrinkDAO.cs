using BusinessObjects;
using System.Globalization;
using System.Text;
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
                _context.SaveChanges();
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
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static Drink? GetDrinkByName(string name)
        {
            string nameToLower = name.ToLower();
            return _context.Drinks.FirstOrDefault(d => d.Name.ToLower().Equals(nameToLower));
        }

        public static List<Drink> SearchDrink(string input)
        {
            List<Drink> results = new List<Drink>();

            foreach (Drink d in _context.Drinks)
            {
                if (RemoveDiacritics(d.Name.ToLower()).Contains(input.ToLower()))
                {
                    results.Add(d);
                }
            }

            return results;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
