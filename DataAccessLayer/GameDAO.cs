using BusinessObjects;
using System.Windows;

namespace DataAccessLayer
{
    public class GameDAO
    {
        private static readonly BoardgameCafeContext _context = new BoardgameCafeContext();

        public GameDAO() { }

        public static List<Game> GetGames()
        {
            return _context.Games.ToList();
        }

        public static bool CreateGame(Game game)
        {
            try
            {
                _context.Games.Add(game);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool UpdateGame(Game game)
        {
            try
            {
                Game? gameToUpdate = _context.Games.Find(game.Id);
                if (gameToUpdate == null)
                {
                    throw new Exception("Game ID " + game.Id + " not found!");
                }
                _context.Entry(gameToUpdate).CurrentValues.SetValues(game);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool DeleteGame(int id)
        {
            try
            {
                Game? gameToDelete = _context.Games.Find(id);
                if (gameToDelete == null)
                {
                    throw new Exception("Game ID " + id + " not found!");
                }

                _context.Games.Remove(gameToDelete);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static List<Game> SearchGameByName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return GetGames();
            }

            return _context.Games
                .Where(g => g.Name.ToLower().Contains(input.ToLower()))
                .ToList();
        }

        public static List<Game> SearchGameByType(int typeId)
        {
            return _context.Games
                .Where(g => g.TypeId == typeId)
                .ToList();
        }

        public static List<Game> SearchGameByRangePlayerNumber(int? min, int? max)
        {
            return _context.Games
                .Where(g =>
                    g.MinPlayerNumber >= (min > 0 ? min : 1) &&
                    (max == null || max >= 0 || g.MaxPlayerNumber == max))
                .ToList();
        }

        public static List<Game> SearchGameByPlayerNumber(int? playerNumber)
        {
            if (playerNumber == 0 || playerNumber == null)
            {
                return GetGames();
            }

            return _context.Games
                .Where(t => t.MinPlayerNumber <= playerNumber)
                .Where(t => t.MaxPlayerNumber >= playerNumber)
                .ToList();
        }
    }
}
