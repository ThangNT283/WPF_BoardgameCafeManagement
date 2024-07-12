using System.Windows;
using System.Windows.Controls;

namespace BoardgameCafeManagement.Views
{
    /// <summary>
    /// Interaction logic for PasswordUserControl.xaml
    /// </summary>
    public partial class PasswordUserControl : UserControl
    {
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordUserControl), new PropertyMetadata(string.Empty));

        public PasswordUserControl()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = PasswordBox.Password;
        }
    }
}
