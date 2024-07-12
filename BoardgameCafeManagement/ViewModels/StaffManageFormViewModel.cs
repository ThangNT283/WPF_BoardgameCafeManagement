using BoardgameCafeManagement.Commands;
using BusinessObjects;
using BusinessObjects.Builders;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BoardgameCafeManagement.ViewModels
{
    public class StaffManageFormViewModel : ViewModelBase
    {
        private readonly IStaffService _staffService;
        private readonly Staff? _selectedStaff;

        private static string SPECIAL_CHAR_PATTERN = @"[!@#$%^&*(),.?""':{}|<>]";
        private static string NUMBER_PATTERN = @"[0-9]";
        private static string LETTER_PATTERN = @"[a-zA-Z]";
        private static string SPACE_PATTERN = @"\s";
        private static string EMAIL_PATTERN = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        #region Fields
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        private string _fullname;
        public string Fullname
        {
            get { return _fullname; }
            set { _fullname = value; OnPropertyChanged(nameof(Fullname)); }
        }
        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged(nameof(Phone)); }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }
        private bool _status;
        public bool Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }
        private string _actionName;
        public string ActionName
        {
            get { return _actionName; }
            set { _actionName = value; OnPropertyChanged(nameof(ActionName)); }
        }
        #endregion

        #region Errors
        private string _usernameError;
        public string UsernameError
        {
            get { return _usernameError; }
            set { _usernameError = value; OnPropertyChanged(nameof(UsernameError)); }
        }
        private string _passwordError;
        public string PasswordError
        {
            get { return _passwordError; }
            set { _passwordError = value; OnPropertyChanged(nameof(PasswordError)); }
        }
        private string _fullnameError;
        public string FullnameError
        {
            get { return _fullnameError; }
            set { _fullnameError = value; OnPropertyChanged(nameof(FullnameError)); }
        }
        private string _phoneError;
        public string PhoneError
        {
            get { return _phoneError; }
            set { _phoneError = value; OnPropertyChanged(nameof(PhoneError)); }
        }
        private string _emailError;
        public string EmailError
        {
            get { return _emailError; }
            set { _emailError = value; OnPropertyChanged(nameof(EmailError)); }
        }
        #endregion

        #region Commands
        public RelayCommand Action { get; }
        #endregion

        public StaffManageFormViewModel(string action, Staff? selectedStaff)
        {
            _staffService = new StaffService();
            ActionName = action.ToUpper();
            _selectedStaff = selectedStaff;
            if (_selectedStaff != null)
            {
                Id = _selectedStaff.Id;
                Username = _selectedStaff.Username;
                Password = _selectedStaff.Password;
                Fullname = _selectedStaff.Fullname;
                Phone = _selectedStaff.Phone;
                Email = _selectedStaff.Email ?? string.Empty;
                Status = _selectedStaff.Status;
            }

            if (action.Equals("create", StringComparison.OrdinalIgnoreCase))
            {
                Action = new RelayCommand(CreateStaff);
            }
            else if (action.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                Action = new RelayCommand(UpdateStaff);
            }
        }

        #region Actions
        private void CreateStaff()
        {
            if (IsValidFields())
            {
                IStaffBuilder builder = new StaffBuilder();
                Staff staff = builder.SetUsername(Username)
                    .SetPassword(Password)
                    .SetFullname(Fullname)
                    .SetPhone(Phone)
                    .SetEmail(Email)
                    .SetStatus(Status)
                    .Build();
                bool isSuccess = _staffService.CreateStaff(staff);

                if (!isSuccess)
                {
                    MessageBox.Show("Failed to create staff!");
                }
                else
                {
                    MessageBox.Show("Create staff successfully!");
                }
            }
        }

        private void UpdateStaff()
        {
            if (IsValidFields())
            {
                IStaffBuilder builder = new StaffBuilder();
                Staff staff = builder.SetId(_selectedStaff.Id)
                    .SetUsername(Username)
                    .SetPassword(Password)
                    .SetFullname(Fullname)
                    .SetPhone(Phone)
                    .SetEmail(Email)
                    .SetStatus(Status)
                    .Build();
                bool isSuccess = _staffService.UpdateStaff(staff);

                if (!isSuccess)
                {
                    MessageBox.Show("Failed to update staff!");
                }
                else
                {
                    MessageBox.Show("Update staff successfully!");
                }
            }
        }
        #endregion

        #region Validating
        private bool IsValidFields()
        {
            bool isValid = true;

            isValid &= ValidateUsername();
            isValid &= ValidatePassword();
            isValid &= ValidateFullname();
            isValid &= ValidatePhone();
            isValid &= ValidateEmail();

            return isValid;
        }

        private bool ValidateUsername()
        {
            if (_username.IsNullOrEmpty())
            {
                UsernameError = "Username cannot be empty";
                return false;
            }

            Username = _username.Trim();
            if (Regex.IsMatch(_username, SPECIAL_CHAR_PATTERN) || Regex.IsMatch(_username, SPACE_PATTERN))
            {
                UsernameError = "No special character or space in username";
                return false;
            }
            if (_username.Length < 6)
            {
                UsernameError = "Username is too short";
                return false;
            }
            if (_username.Length > 64)
            {
                UsernameError = "Username is too long";
                return false;
            }
            if (_staffService.GetStaffs().ToList()
                .Find(s => s.Username == _username &&
                    (_selectedStaff == null || _selectedStaff.Username != s.Username)) is not null)
            {
                PhoneError = "Username is in used";
                return false;
            }

            UsernameError = string.Empty;
            return true;
        }

        private bool ValidatePassword()
        {
            if (_password.IsNullOrEmpty())
            {
                PasswordError = "Password cannot be empty";
                return false;
            }
            if (_password.Length < 6)
            {
                PhoneError = "Password is too short";
                return false;
            }
            if (_password.Length > 128)
            {
                PasswordError = "Password is too long";
                return false;
            }

            PasswordError = string.Empty;
            return true;
        }

        private bool ValidateFullname()
        {
            if (_fullname.IsNullOrEmpty())
            {
                FullnameError = "Fullname cannot be empty";
                return false;
            }

            Fullname = Regex.Replace(_fullname.Trim(), @"\s+", " ");
            if (Regex.IsMatch(_fullname, SPECIAL_CHAR_PATTERN) || Regex.IsMatch(_fullname, NUMBER_PATTERN))
            {
                FullnameError = "No special character or number in full name";
                return false;
            }
            if (_fullname.Length > 64)
            {
                FullnameError = "Fullname is too long";
                return false;
            }

            FullnameError = string.Empty;
            return true;
        }

        private bool ValidatePhone()
        {
            if (_phone.IsNullOrEmpty())
            {
                PhoneError = "Phone cannot be empty";
                return false;
            }

            Phone = Regex.Replace(_phone.Trim(), @"\s+", " ");
            if (Regex.IsMatch(_phone, SPECIAL_CHAR_PATTERN) || Regex.IsMatch(_phone, LETTER_PATTERN))
            {
                PhoneError = "No special character or letter in phone";
                return false;
            }
            if (_phone.Length != 10)
            {
                PhoneError = "Phone must have 10 digits";
                return false;
            }
            if (_staffService.GetStaffs().ToList()
                .Find(s => s.Phone == _phone &&
                    (_selectedStaff == null || _selectedStaff.Id != s.Id)) is not null)
            {
                PhoneError = "Phone is in used";
                return false;
            }

            PhoneError = string.Empty;
            return true;
        }

        private bool ValidateEmail()
        {
            if (_email.IsNullOrEmpty())
            {
                EmailError = "Email cannot be empty";
                return false;
            }

            Email = _email.Trim();
            if (!Regex.IsMatch(_email, EMAIL_PATTERN))
            {
                EmailError = "Invalid email";
                return false;
            }
            if (_email.Length > 320)
            {
                EmailError = "Email is too long";
                return false;
            }
            if (_staffService.GetStaffs().ToList()
                    .Find(s => s.Email == _email &&
                        (_selectedStaff == null || s.Id != _selectedStaff.Id)) is not null)
            {
                EmailError = "Email is in used";
                return false;
            }

            EmailError = string.Empty;
            return true;
        }
        #endregion
    }
}