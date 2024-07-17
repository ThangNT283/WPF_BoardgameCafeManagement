using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        public List<Drink> GetDrinks() => DrinkDAO.GetDrinks();
        public bool CreateDrink(Drink drink) => DrinkDAO.CreateDrink(drink);
        public bool UpdateDrink(Drink drink) => DrinkDAO.UpdateDrink(drink);
        public Drink? GetDrinkByName(string name) => DrinkDAO.GetDrinkByName(name);
        public List<Drink> SearchDrink(string input) => DrinkDAO.SearchDrink(input);
    }
}
