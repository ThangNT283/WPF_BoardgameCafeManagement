using BoardgameCafeManagement.ViewModels;
using BusinessObjects;
using System.Windows;

namespace BoardgameCafeManagement.Views
{
    public partial class DrinkManageForm : Window
    {
        private readonly DrinkManageView _drinkManageView;

        public DrinkManageForm(DrinkManageView drinkManageView, string action, Drink selectedDrink)
        {
            InitializeComponent();
            DataContext = new DrinkManageFormViewModel(action, selectedDrink);
            _drinkManageView = drinkManageView;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dataContext = _drinkManageView.DataContext as DrinkManageViewModel;
            if (dataContext != null)
            {
                dataContext.RefreshList.Execute(null);
            }
        }
    }
}
