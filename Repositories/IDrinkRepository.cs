using BusinessObjects;

namespace Repositories
{
    public interface IDrinkRepository
    {
        List<Drink> GetDrinks();
        bool CreateDrink(Drink drink);
        bool UpdateDrink(Drink drink);
    }
}
