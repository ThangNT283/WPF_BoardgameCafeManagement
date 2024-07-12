namespace BusinessObjects.Builders
{
    public class TableBuilder : ITableBuilder
    {
        private Table _table;

        public TableBuilder()
        {
            _table = new Table();
        }

        public Table Build() => _table;

        public ITableBuilder SetId(int id)
        {
            _table.Id = id;
            return this;
        }

        public ITableBuilder SetTableNumber(string tableNumber)
        {
            _table.TableNumber = tableNumber;
            return this;
        }

        public ITableBuilder SetCapacity(int capacity)
        {
            _table.Capacity = capacity;
            return this;
        }

        public ITableBuilder SetStatus(bool status)
        {
            _table.Status = status;
            return this;
        }

        public ITableBuilder SetBills(ICollection<Bill> bills)
        {
            _table.Bills = bills;
            return this;
        }
    }
}
