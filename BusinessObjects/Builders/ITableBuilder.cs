namespace BusinessObjects.Builders
{
    public interface ITableBuilder
    {
        ITableBuilder SetId(int id);
        ITableBuilder SetTableNumber(string tableNumber);
        ITableBuilder SetCapacity(int capacity);
        ITableBuilder SetStatus(bool status);
        ITableBuilder SetBills(ICollection<Bill> bills);
        Table Build();
    }
}
