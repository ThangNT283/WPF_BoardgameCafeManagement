using BusinessObjects;

namespace Repositories
{
    public interface IDrinkRepository
    {
        List<Drink> GetDrinks();
        bool CreateDrink(Drink drink);
        bool UpdateDrink(Drink drink);
        Drink? GetDrinkByName(string name);
        List<Drink> SearchDrink(string input);
    }
}
