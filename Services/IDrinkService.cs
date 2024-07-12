using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IDrinkService
    {
        ObservableCollection<Drink> GetDrinks();
        bool CreateDrink(Drink drink);
        bool UpdateDrink(Drink drink);
    }
}
