using BoardgameCafeManagement.ViewModels;
using BusinessObjects;
using System.Windows;

namespace BoardgameCafeManagement.Views
{
    public partial class GameManageForm : Window
    {
        private readonly GameManageView _gameManageView;

        public GameManageForm(GameManageView gameManageView, string action, Game selectedGame)
        {
            InitializeComponent();
            DataContext = new GameManageFormViewModel(action, selectedGame);
            _gameManageView = gameManageView;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dataContext = _gameManageView.DataContext as GameManageViewModel;
            if (dataContext != null)
            {
                dataContext.RefreshList.Execute(null);
            }
        }
    }
}
