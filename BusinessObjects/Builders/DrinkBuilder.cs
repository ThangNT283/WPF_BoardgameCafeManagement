namespace BusinessObjects.Builders
{
    public class DrinkBuilder : IDrinkBuilder
    {
        private Drink _drink;

        public DrinkBuilder()
        {
            _drink = new Drink();
        }

        public Drink Build() => _drink;
        public IDrinkBuilder SetId(int id)
        {
            _drink.Id = id;
            return this;
        }
        public IDrinkBuilder SetName(string name)
        {
            _drink.Name = name;
            return this;
        }
        public IDrinkBuilder SetCategoryId(int categoryId)
        {
            _drink.CategoryId = categoryId;
            return this;
        }
        public IDrinkBuilder SetStatus(bool status)
        {
            _drink.Status = status;
            return this;
        }
        public IDrinkBuilder SetCategory(DrinkCategory category)
        {
            _drink.Category = category;
            return this;
        }
    }
}
