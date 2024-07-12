using BusinessObjects;

namespace Repositories
{
    public interface ITableRepository
    {
        List<Table> GetTables();
        bool CreateTable(Table table);
        bool UpdateTable(Table table);
        List<Table> SearchTableByNumber(string input);
        List<Table> SearchTableByCapacity(int num);
        List<Table> GetBlankTables();
    }
}
