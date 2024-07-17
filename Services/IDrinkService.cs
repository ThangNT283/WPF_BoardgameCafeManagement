using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IDrinkService
    {
        ObservableCollection<Drink> GetDrinks();
        bool CreateDrink(Drink drink);
        bool UpdateDrink(Drink drink);
        Drink? GetDrinkByName(string name);
        ObservableCollection<Drink> SearchDrink(string input);
    }
}
