using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        public List<Drink> GetDrinks() => DrinkDAO.GetDrinks();
        public bool CreateDrink(Drink drink) => DrinkDAO.CreateDrink(drink);
        public bool UpdateDrink(Drink drink) => DrinkDAO.UpdateDrink(drink);
    }
}
