namespace BusinessObjects.Builders
{
    public class BillBuilder : IBillBuilder
    {
        private Bill _bill;

        public BillBuilder()
        {
            _bill = new Bill();
        }

        public IBillBuilder SetId(int id)
        {
            _bill.Id = id;
            return this;
        }
        public IBillBuilder SetTableId(int tableId)
        {
            _bill.TableId = tableId;
            return this;
        }
        public IBillBuilder SetCustomerName(string customerName)
        {
            _bill.CustomerName = customerName;
            return this;
        }
        public IBillBuilder SetNumberOfGames(int numberOfGames)
        {
            _bill.NumberOfGames = numberOfGames;
            return this;
        }
        public Bill Build() => _bill;
    }
}
