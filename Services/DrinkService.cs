using BusinessObjects;
using Repositories;
using System.Collections.ObjectModel;

namespace Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepository _drinkRepo;

        public DrinkService()
        {
            _drinkRepo = new DrinkRepository();
        }

        public ObservableCollection<Drink> GetDrinks()
        {
            return new ObservableCollection<Drink>(_drinkRepo.GetDrinks());
        }
        public bool CreateDrink(Drink drink) => _drinkRepo.CreateDrink(drink);
        public bool UpdateDrink(Drink drink) => _drinkRepo.UpdateDrink(drink);
        public Drink? GetDrinkByName(string name) => _drinkRepo.GetDrinkByName(name);
        public ObservableCollection<Drink> SearchDrink(string input)
        {
            return new ObservableCollection<Drink>(_drinkRepo.SearchDrink(input));
        }
    }
}
