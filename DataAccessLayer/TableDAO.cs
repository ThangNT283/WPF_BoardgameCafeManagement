using BusinessObjects;
using System.Windows;

namespace DataAccessLayer
{
    public class TableDAO
    {
        private static readonly BoardgameCafeContext _context = new BoardgameCafeContext();

        public TableDAO()
        {
        }

        public static List<Table> GetTables()
        {
            return _context.Tables.ToList();
        }

        public static bool CreateTable(Table table)
        {
            try
            {
                _context.Tables.Add(table);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool UpdateTable(Table table)
        {
            try
            {
                Table? tableToUpdate = _context.Tables.Find(table.Id);
                if (tableToUpdate == null)
                {
                    throw new Exception("Table ID " + table.Id + " not found!");
                }

                _context.Entry(tableToUpdate).CurrentValues.SetValues(table);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static List<Table> SearchTableByNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return GetTables();
            }

            return _context.Tables
                .Where(t => t.TableNumber.ToLower().Contains(input.ToLower()))
                .ToList();
        }

        public static List<Table> SearchTableByCapacity(int num)
        {
            return _context.Tables
                .Where(t => t.Capacity >= num)
                .ToList();
        }

        public static List<Table> GetBlankTables()
        {
            return _context.Tables
                .Where(t => t.Status == false)
                .ToList();
        }

        public static List<Table> GetInUsedTables()
        {
            return _context.Tables
                .Where(t => t.Status == true)
                .ToList();
        }
    }
}
