namespace BusinessObjects.Builders
{
    public class DrinkVariationBuilder : IDrinkVariationBuilder
    {
        private DrinkVariation _drinkVariation;

        public DrinkVariationBuilder()
        {
            _drinkVariation = new DrinkVariation();
        }

        public DrinkVariation Build() => _drinkVariation;
        public IDrinkVariationBuilder SetId(int id)
        {
            _drinkVariation.Id = id;
            return this;
        }
        public IDrinkVariationBuilder SetDrinkId(int drinkId)
        {
            _drinkVariation.DrinkId = drinkId;
            return this;
        }
        public IDrinkVariationBuilder SetName(string name)
        {
            _drinkVariation.VariationName = name;
            return this;
        }
        public IDrinkVariationBuilder SetPrice(int price)
        {
            _drinkVariation.Price = price;
            return this;
        }
        public IDrinkVariationBuilder SetCreatedAt(DateTime createdAt)
        {
            _drinkVariation.CreatedAt = createdAt;
            return this;
        }
        public IDrinkVariationBuilder SetStatus(bool status)
        {
            _drinkVariation.Status = status;
            return this;
        }
    }
}
