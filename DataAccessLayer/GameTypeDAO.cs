using BusinessObjects;
using System.Windows;

namespace DataAccessLayer
{
    public class GameTypeDAO
    {
        private static readonly BoardgameCafeContext _context = new BoardgameCafeContext();

        public GameTypeDAO() { }

        public static List<GameType> GetGameTypes()
        {
            return _context.GameTypes.ToList();
        }

        public static bool CreateGameType(GameType gameType)
        {
            try
            {
                _context.GameTypes.Add(gameType);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool UpdateGameType(GameType gameType)
        {
            try
            {
                GameType? typeToUpdate = _context.GameTypes.Find(gameType.Id);
                if (typeToUpdate == null)
                {
                    throw new Exception("Game type ID " + gameType.Id + " not found!");
                }
                _context.Entry(typeToUpdate).CurrentValues.SetValues(gameType);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool DeleteGameType(int typeId)
        {
            try
            {
                GameType? typeToDelete = _context.GameTypes.Find(typeId);
                if (typeToDelete == null)
                {
                    throw new Exception("Game type ID " + typeId + " not found!");
                }

                _context.GameTypes.Remove(typeToDelete);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static List<GameType> SearchGameType(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return GetGameTypes();
            }

            return _context.GameTypes
                .Where(t => t.Name.Contains(input, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
