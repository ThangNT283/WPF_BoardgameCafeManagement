using BoardgameCafeManagement.Commands;
using BusinessObjects;
using Microsoft.Extensions.Configuration;
using Repositories;
using Services;
using System.Windows;

namespace BoardgameCafeManagement.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IConfiguration _configuration;
        private readonly IStaffService _staffServ;

        private string _credential;
        public string Credential
        {
            get { return _credential; }
            set { _credential = value; OnPropertyChanged(nameof(Credential)); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public RelayCommand LoginCommand { get; }

        public LoginViewModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _staffServ = new StaffService();

            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            try
            {
                if (_credential == null || _password == null)
                {
                    MessageBox.Show("All fields must be filled!");
                    return;
                }

                string? adminInfo = _configuration["AdminAccount:Username"];
                string? adminPassword = _configuration["AdminAccount:Password"];
                if (adminInfo != null && adminPassword != null &&
                    _credential == adminInfo && _password == adminPassword)
                {
                    MessageBox.Show("Login successfully!");
                    //this.Hide();
                    //AdminWindow mainWindow = new AdminWindow();
                    //mainWindow.Show();
                    return;
                }

                Staff? user = _staffServ.GetStaffs().FirstOrDefault(u =>
                    u.Email == _credential || u.Username == _credential);
                if (user != null && user.Password == _password)
                {
                    if (!user.Status)
                    {
                        MessageBox.Show("This account is deleted!");
                        return;
                    }

                    Application.Current.Properties["User"] = _credential;

                    MessageBox.Show("Login successfully!");
                    //CustomerWindow customerWindow = new CustomerWindow();
                    //customerWindow.Show();
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("Email or password is incorrect!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
