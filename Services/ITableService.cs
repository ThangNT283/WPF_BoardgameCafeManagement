using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface ITableService
    {
        ObservableCollection<Table> GetTables();
        bool CreateTable(Table table);
        bool UpdateTable(Table table);
        ObservableCollection<Table> SearchTableByNumber(string input);
        ObservableCollection<Table> SearchTableByCapacity(int num);
        ObservableCollection<Table> GetBlankTables();
        ObservableCollection<Table> GetInUsedTables();
    }
}
