using BoardgameCafeManagement.Commands;
using BusinessObjects;
using Repositories;

namespace BoardgameCafeManagement.ViewModels
{
    public class DrinkManageFormViewModel : ViewModelBase
    {
        private readonly IDrinkService _drinkService;
        private readonly Drink? _selectedDrink;

        private static string SPECIAL_CHAR_PATTERN = @"[!@#$%^&*(),.?""':{}|<>]";
        private static string LETTER_PATTERN = @"[a-zA-Z]";
        private static string SPACE_PATTERN = @"\s";

        #region Fields
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }
        private string _drinkNumber;
        public string DrinkNumber
        {
            get { return _drinkNumber; }
            set { _drinkNumber = value; OnPropertyChanged(nameof(DrinkNumber)); }
        }
        private string _capacity;
        public string Capacity
        {
            get { return _capacity; }
            set { _capacity = value; OnPropertyChanged(nameof(Capacity)); }
        }
        private int _capacityValue = 0;
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
        private string _drinkNumberError;
        public string DrinkNumberError
        {
            get { return _drinkNumberError; }
            set { _drinkNumberError = value; OnPropertyChanged(nameof(DrinkNumberError)); }
        }
        private string _capacityError;
        public string CapacityError
        {
            get { return _capacityError; }
            set { _capacityError = value; OnPropertyChanged(nameof(CapacityError)); }
        }
        #endregion

        #region Commands
        public RelayCommand Action { get; }
        #endregion

        public DrinkManageFormViewModel(string action, Drink? selectedDrink)
        {
            //_drinkService = new DrinkService();
            //ActionName = action.ToUpper();
            //_selectedDrink = selectedDrink;
            //if (_selectedDrink != null)
            //{
            //    Id = _selectedDrink.Id;
            //    DrinkNumber = _selectedDrink.DrinkNumber;
            //    Capacity = _selectedDrink.Capacity.ToString();
            //    Status = _selectedDrink.Status;
            //}

            //if (action.Equals("create", StringComparison.OrdinalIgnoreCase))
            //{
            //    Action = new RelayCommand(CreateDrink);
            //}
            //else if (action.Equals("update", StringComparison.OrdinalIgnoreCase))
            //{
            //    Action = new RelayCommand(UpdateDrink);
            //}
        }

        //#region Actions
        //private void CreateDrink()
        //{
        //    if (IsValidFields())
        //    {
        //        IDrinkBuilder builder = new DrinkBuilder();
        //        Drink drink = builder.SetDrinkNumber(DrinkNumber)
        //            .SetCapacity(_capacityValue)
        //            .SetStatus(Status)
        //            .Build();
        //        bool isSuccess = _drinkService.CreateDrink(drink);

        //        if (!isSuccess)
        //        {
        //            MessageBox.Show("Failed to create drink!");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Create drink successfully!");
        //        }
        //    }
        //}

        //private void UpdateDrink()
        //{
        //    if (IsValidFields())
        //    {
        //        IDrinkBuilder builder = new DrinkBuilder();
        //        Drink drink = builder.SetId(_selectedDrink.Id)
        //            .SetDrinkNumber(DrinkNumber)
        //            .SetCapacity(_capacityValue)
        //            .SetStatus(Status)
        //            .Build();
        //        bool isSuccess = _drinkService.UpdateDrink(drink);

        //        if (!isSuccess)
        //        {
        //            MessageBox.Show("Failed to update drink!");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Update drink successfully!");
        //        }
        //    }
        //}
        //#endregion

        //#region Validating
        //private bool IsValidFields()
        //{
        //    bool isValid = true;

        //    isValid &= ValidateDrinkNumber();
        //    isValid &= ValidateCapacity();

        //    return isValid;
        //}

        //private bool ValidateDrinkNumber()
        //{
        //    if (_drinkNumber.IsNullOrEmpty())
        //    {
        //        DrinkNumberError = "Drink number cannot be empty";
        //        return false;
        //    }

        //    DrinkNumber = _drinkNumber.Trim();
        //    if (Regex.IsMatch(_drinkNumber, SPECIAL_CHAR_PATTERN) || Regex.IsMatch(_drinkNumber, SPACE_PATTERN))
        //    {
        //        DrinkNumberError = "No special character or space in drink number";
        //        return false;
        //    }
        //    if (_drinkNumber.Length != 4)
        //    {
        //        DrinkNumberError = "Drink number must have length of 4";
        //        return false;
        //    }
        //    if (_drinkService.GetDrinks().ToList()
        //        .Find(t => t.DrinkNumber == _drinkNumber &&
        //            (_selectedDrink == null || t.Id != _selectedDrink.Id)) is not null)
        //    {
        //        DrinkNumberError = "Drink number cannot be duplicated";
        //        return false;
        //    }

        //    DrinkNumberError = string.Empty;
        //    return true;
        //}

        //private bool ValidateCapacity()
        //{
        //    if (_capacity.IsNullOrEmpty())
        //    {
        //        CapacityError = "Capacity cannot be empty";
        //        return false;
        //    }

        //    Capacity = _capacity.Trim();
        //    if (Regex.IsMatch(_capacity, SPECIAL_CHAR_PATTERN) ||
        //        Regex.IsMatch(_capacity, SPACE_PATTERN) ||
        //        Regex.IsMatch(_capacity, LETTER_PATTERN))
        //    {
        //        CapacityError = "Capacity can only be number";
        //        return false;
        //    }

        //    try
        //    {
        //        _capacityValue = Convert.ToInt32(_capacity);
        //        if (_capacityValue <= 0)
        //        {
        //            CapacityError = "Capacity must be larger than 0";
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CapacityError = "Invalid capacity";
        //        return false;
        //    }

        //    CapacityError = string.Empty;
        //    return true;
        //}
        //#endregion
    }
}