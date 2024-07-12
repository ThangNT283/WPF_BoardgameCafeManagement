using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class TableRepository : ITableRepository
    {
        public List<Table> GetTables() => TableDAO.GetTables();
        public bool CreateTable(Table table) => TableDAO.CreateTable(table);
        public bool UpdateTable(Table table) => TableDAO.CreateTable(table);
        public List<Table> SearchTableByNumber(string input) => TableDAO.SearchTableByNumber(input);
        public List<Table> SearchTableByCapacity(int num) => TableDAO.SearchTableByCapacity(num);
        public List<Table> GetBlankTables() => TableDAO.GetBlankTables();
    }
}
