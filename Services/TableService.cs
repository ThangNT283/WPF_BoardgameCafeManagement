using BusinessObjects;
using Repositories;
using System.Collections.ObjectModel;

namespace Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepo;

        public TableService()
        {
            _tableRepo = new TableRepository();
        }

        public ObservableCollection<Table> GetTables()
        {
            return new ObservableCollection<Table>(_tableRepo.GetTables());
        }
        public bool CreateTable(Table table) => _tableRepo.CreateTable(table);
        public bool UpdateTable(Table table) => _tableRepo.UpdateTable(table);
        public ObservableCollection<Table> SearchTableByNumber(string input)
        {
            return new ObservableCollection<Table>(_tableRepo.SearchTableByNumber(input));
        }
        public ObservableCollection<Table> SearchTableByCapacity(int num)
        {
            return new ObservableCollection<Table>(_tableRepo.SearchTableByCapacity(num));
        }
        public ObservableCollection<Table> GetBlankTables()
        {
            return new ObservableCollection<Table>(_tableRepo.GetBlankTables());
        }
        public ObservableCollection<Table> GetInUsedTables()
        {
            return new ObservableCollection<Table>(_tableRepo.GetInUsedTables());
        }
    }
}
