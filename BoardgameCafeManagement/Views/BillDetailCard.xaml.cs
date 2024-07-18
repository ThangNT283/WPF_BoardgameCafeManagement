using BoardgameCafeManagement.ViewModels;
using BusinessObjects;
using System.Windows;

namespace BoardgameCafeManagement.Views
{
    public partial class BillDetailCard : Window
    {
        public BillDetailCard(Bill bill)
        {
            InitializeComponent();
            DataContext = new BillDetailCardViewModel(bill);
        }
    }
}
