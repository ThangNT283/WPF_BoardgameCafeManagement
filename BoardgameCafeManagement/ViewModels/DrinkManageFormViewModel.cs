using BoardgameCafeManagement.Commands;
using BusinessObjects;
using BusinessObjects.Builders;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace BoardgameCafeManagement.ViewModels
{
    public class DrinkManageFormViewModel : ViewModelBase
    {
        private readonly IDrinkService _drinkService;
        private readonly IDrinkCategoryService _drinkCategoryService;
        private readonly IDrinkVariationService _drinkVariationService;
        private readonly Drink? _selectedDrink;

        private static string SPECIAL_CHAR_PATTERN = @"[!@#$%^&*(),.?""':{}|<>]";
        private static string NUMBER_PATTERN = @"[0-9]";
        private static string LETTER_PATTERN = @"[a-zA-Z]";
        private static string SPACE_PATTERN = @"\s";

        #region Properties
        private List<DrinkCategory> _categories;
        public List<DrinkCategory> Categories
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged(nameof(Categories)); }
        }
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }
        private string _drinkName;
        public string DrinkName
        {
            get { return _drinkName; }
            set { _drinkName = value; OnPropertyChanged(nameof(DrinkName)); }
        }
        private int _categoryId;
        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; OnPropertyChanged(nameof(CategoryId)); }
        }
        private bool _status;
        public bool Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }
        private string? _sPrice;
        public string? SPrice
        {
            get { return _sPrice; }
            set { _sPrice = value; OnPropertyChanged(nameof(SPrice)); }
        }
        private string? _mPrice;
        public string? MPrice
        {
            get { return _mPrice; }
            set { _mPrice = value; OnPropertyChanged(nameof(MPrice)); }
        }
        private string? _lPrice;
        public string? LPrice
        {
            get { return _lPrice; }
            set { _lPrice = value; OnPropertyChanged(nameof(LPrice)); }
        }
        private string _actionName;
        public string ActionName
        {
            get { return _actionName; }
            set { _actionName = value; OnPropertyChanged(nameof(ActionName)); }
        }
        #endregion

        #region Errors
        private string _drinkNameError;
        public string DrinkNameError
        {
            get { return _drinkNameError; }
            set { _drinkNameError = value; OnPropertyChanged(nameof(DrinkNameError)); }
        }
        private string _sPriceError;
        public string SPriceError
        {
            get { return _sPriceError; }
            set { _sPriceError = value; OnPropertyChanged(nameof(SPriceError)); }
        }
        private string _mPriceError;
        public string MPriceError
        {
            get { return _mPriceError; }
            set { _mPriceError = value; OnPropertyChanged(nameof(MPriceError)); }
        }
        private string _lPriceError;
        public string LPriceError
        {
            get { return _lPriceError; }
            set { _lPriceError = value; OnPropertyChanged(nameof(LPriceError)); }
        }
        #endregion

        #region Commands
        public RelayCommand Action { get; }
        #endregion

        public DrinkManageFormViewModel(string action, Drink? selectedDrink)
        {
            _drinkService = new DrinkService();
            _drinkCategoryService = new DrinkCategoryService();
            _drinkVariationService = new DrinkVariationService();
            ActionName = action.ToUpper();
            _categories = _drinkCategoryService.GetDrinkCategories();

            _selectedDrink = selectedDrink;
            if (_selectedDrink != null)
            {
                Id = _selectedDrink.Id;
                DrinkName = _selectedDrink.Name;
                CategoryId = _selectedDrink.CategoryId;
                Status = _selectedDrink.Status;

                ObservableCollection<DrinkVariation> drinkVariations = _drinkVariationService.GetVariationsByDrinkId(_selectedDrink.Id);
                foreach (DrinkVariation v in drinkVariations)
                {
                    if (v.VariationName == "Size S") SPrice = v.Price.ToString();
                    if (v.VariationName == "Size M") MPrice = v.Price.ToString();
                    if (v.VariationName == "Size L") LPrice = v.Price.ToString();
                }
            }

            if (action.Equals("create", StringComparison.OrdinalIgnoreCase))
            {
                Action = new RelayCommand(CreateDrink);
            }
            else if (action.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                Action = new RelayCommand(UpdateDrink);
            }
        }

        #region Actions
        private void CreateDrink()
        {
            try
            {
                if (IsValidFields())
                {
                    IDrinkBuilder drinkBuilder = new DrinkBuilder();
                    Drink drink = drinkBuilder.SetName(DrinkName)
                        .SetCategoryId(CategoryId)
                        .SetStatus(Status)
                        .Build();
                    bool isSuccess = _drinkService.CreateDrink(drink);

                    if (!isSuccess)
                    {
                        MessageBox.Show("Failed to create drink!");
                    }
                    else
                    {
                        isSuccess = true;
                        var drinkId = _drinkService.GetDrinkByName(DrinkName).Id;

                        if (SPrice != null)
                        {
                            IDrinkVariationBuilder drinkVariationBuilder = new DrinkVariationBuilder();
                            DrinkVariation drinkVariation = drinkVariationBuilder
                                .SetDrinkId(drinkId)
                                .SetName("Size S")
                                .SetPrice(Convert.ToInt32(SPrice))
                                .SetStatus(true)
                                .Build();
                            MessageBox.Show(drinkVariation.ToString());
                            isSuccess &= _drinkVariationService.CreateVariation(drinkVariation);
                        }

                        if (MPrice != null)
                        {
                            IDrinkVariationBuilder drinkVariationBuilder = new DrinkVariationBuilder();
                            DrinkVariation drinkVariation = drinkVariationBuilder
                                .SetDrinkId(drinkId)
                                .SetName("Size M")
                                .SetPrice(Convert.ToInt32(MPrice))
                                .SetStatus(true)
                                .Build();
                            MessageBox.Show(drinkVariation.ToString());
                            isSuccess &= _drinkVariationService.CreateVariation(drinkVariation);
                        }

                        if (LPrice != null)
                        {
                            IDrinkVariationBuilder drinkVariationBuilder = new DrinkVariationBuilder();
                            DrinkVariation drinkVariation = drinkVariationBuilder
                                .SetDrinkId(drinkId)
                                .SetName("Size L")
                                .SetPrice(Convert.ToInt32(LPrice))
                                .SetStatus(true)
                                .Build();
                            MessageBox.Show(drinkVariation.ToString());
                            isSuccess &= _drinkVariationService.CreateVariation(drinkVariation);
                        }

                        if (isSuccess)
                        {
                            MessageBox.Show("Create drink successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to create drink variations!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateDrink()
        {
            try
            {
                if (IsValidFields())
                {
                    IDrinkBuilder drinkBuilder = new DrinkBuilder();
                    Drink drink = drinkBuilder.SetId(_selectedDrink.Id)
                        .SetName(DrinkName)
                        .SetCategoryId(CategoryId)
                        .SetStatus(Status)
                        .Build();
                    bool isSuccess = _drinkService.UpdateDrink(drink);

                    if (!isSuccess)
                    {
                        MessageBox.Show("Failed to update drink!");
                    }
                    else
                    {
                        isSuccess = true;

                        List<DrinkVariation> variations = _drinkVariationService.GetVariationsByDrinkId(_selectedDrink.Id).ToList();
                        DrinkVariation s = variations.FirstOrDefault(s => s.VariationName == "Size S");
                        DrinkVariation m = variations.FirstOrDefault(s => s.VariationName == "Size M");
                        DrinkVariation l = variations.FirstOrDefault(s => s.VariationName == "Size L");

                        if (!SPrice.IsNullOrEmpty())
                        {
                            IDrinkVariationBuilder drinkVariationBuilder = new DrinkVariationBuilder();
                            if (s != null)
                            {
                                isSuccess &= _drinkVariationService.UpdateVariation(drinkVariationBuilder
                                    .SetId(s.Id)
                                    .SetDrinkId(_drinkService.GetDrinkByName(DrinkName).Id)
                                    .SetName("Size S")
                                    .SetPrice(Convert.ToInt32(SPrice))
                                    .SetStatus(Status)
                                    .SetCreatedAt(s.CreatedAt)
                                    .Build());
                            }
                            else
                            {
                                isSuccess &= _drinkVariationService.CreateVariation(drinkVariationBuilder
                                    .SetDrinkId(_drinkService.GetDrinkByName(DrinkName).Id)
                                    .SetName("Size S")
                                    .SetPrice(Convert.ToInt32(SPrice))
                                    .SetCreatedAt(s.CreatedAt)
                                    .SetStatus(Status)
                                    .Build());
                            }
                        }
                        else
                        {
                            if (s != null)
                            {
                                IDrinkVariationBuilder drinkVariationBuilder = new DrinkVariationBuilder();
                                isSuccess &= _drinkVariationService.DeleteVariation(s.Id);
                            }
                        }

                        if (!MPrice.IsNullOrEmpty())
                        {
                            IDrinkVariationBuilder drinkVariationBuilder = new DrinkVariationBuilder();
                            if (m != null)
                            {
                                isSuccess &= _drinkVariationService.UpdateVariation(drinkVariationBuilder
                                    .SetId(m.Id)
                                    .SetDrinkId(_drinkService.GetDrinkByName(DrinkName).Id)
                                    .SetName("Size M")
                                    .SetPrice(Convert.ToInt32(MPrice))
                                    .SetStatus(Status)
                                    .SetCreatedAt(m.CreatedAt)
                                    .Build());
                            }
                            else
                            {
                                isSuccess &= _drinkVariationService.CreateVariation(drinkVariationBuilder
                                    .SetDrinkId(_drinkService.GetDrinkByName(DrinkName).Id)
                                    .SetName("Size M")
                                    .SetPrice(Convert.ToInt32(MPrice))
                                    .SetStatus(Status)
                                    .SetCreatedAt(m.CreatedAt)
                                    .Build());
                            }
                        }
                        else
                        {
                            if (m != null)
                            {
                                IDrinkVariationBuilder drinkVariationBuilder = new DrinkVariationBuilder();
                                isSuccess &= _drinkVariationService.DeleteVariation(m.Id);
                            }
                        }

                        if (!LPrice.IsNullOrEmpty())
                        {
                            IDrinkVariationBuilder drinkVariationBuilder = new DrinkVariationBuilder();
                            if (l != null)
                            {
                                isSuccess &= _drinkVariationService.UpdateVariation(drinkVariationBuilder
                                    .SetId(l.Id)
                                    .SetDrinkId(_drinkService.GetDrinkByName(DrinkName).Id)
                                    .SetName("Size L")
                                    .SetPrice(Convert.ToInt32(LPrice))
                                    .SetStatus(Status)
                                    .SetCreatedAt(l.CreatedAt)
                                    .Build());
                            }
                            else
                            {
                                isSuccess &= _drinkVariationService.CreateVariation(drinkVariationBuilder
                                    .SetDrinkId(_drinkService.GetDrinkByName(DrinkName).Id)
                                    .SetName("Size L")
                                    .SetPrice(Convert.ToInt32(LPrice))
                                    .SetStatus(Status)
                                    .SetCreatedAt(l.CreatedAt)
                                    .Build());
                            }
                        }
                        else
                        {
                            if (m != null)
                            {
                                IDrinkVariationBuilder drinkVariationBuilder = new DrinkVariationBuilder();
                                isSuccess &= _drinkVariationService.DeleteVariation(l.Id);
                            }
                        }

                        if (isSuccess)
                        {
                            MessageBox.Show("Update drink successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update drink variations!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";
                var errorMessage = $"{ex.Message}\nInner Exception: {innerExceptionMessage}\n{ex.StackTrace}";
                MessageBox.Show(errorMessage);
            }
        }
        #endregion

        #region Validating
        private bool IsValidFields()
        {
            bool isValid = true;

            isValid &= ValidateDrinkName();
            isValid &= ValidatePrices();

            if (isValid)
            {
                isValid &= ValidateSPrice();
                isValid &= ValidateMPrice();
                isValid &= ValidateLPrice();
            }

            return isValid;
        }

        private bool ValidateDrinkName()
        {
            if (_drinkName.IsNullOrEmpty())
            {
                DrinkNameError = "Drink name cannot be empty";
                return false;
            }

            DrinkName = Regex.Replace(_drinkName.Trim(), @"\s+", " ");
            if (Regex.IsMatch(_drinkName, SPECIAL_CHAR_PATTERN) || Regex.IsMatch(_drinkName, NUMBER_PATTERN))
            {
                DrinkNameError = "No special character or number in drink name";
                return false;
            }
            if (_drinkName.Length > 128)
            {
                DrinkNameError = "Drink name is too long";
                return false;
            }

            DrinkNameError = string.Empty;
            return true;
        }

        private bool ValidatePrices()
        {
            if (_sPrice.IsNullOrEmpty() && _mPrice.IsNullOrEmpty() && _lPrice.IsNullOrEmpty())
            {
                SPriceError = "Price cannot be empty";
                MPriceError = "Price cannot be empty";
                LPriceError = "Price cannot be empty";
                return false;
            }

            return true;
        }

        private bool ValidateSPrice()
        {
            SPrice = _sPrice.Trim();
            if (Regex.IsMatch(_sPrice, SPECIAL_CHAR_PATTERN) ||
                Regex.IsMatch(_sPrice, SPACE_PATTERN) ||
                Regex.IsMatch(_sPrice, LETTER_PATTERN))
            {
                SPriceError = "Price can only be number";
                return false;
            }

            if (_sPrice.IsNullOrEmpty()) { return true; }

            try
            {
                if (Convert.ToInt32(_sPrice) <= 0)
                {
                    SPriceError = "Capacity must be larger than 0";
                    return false;
                }
            }
            catch (Exception ex)
            {
                SPriceError = "Invalid price";
                MessageBox.Show(ex.Message);
                return false;
            }

            SPriceError = string.Empty;
            return true;
        }

        private bool ValidateMPrice()
        {
            MPrice = _mPrice.Trim();
            if (Regex.IsMatch(_mPrice, SPECIAL_CHAR_PATTERN) ||
                Regex.IsMatch(_mPrice, SPACE_PATTERN) ||
                Regex.IsMatch(_mPrice, LETTER_PATTERN))
            {
                MPriceError = "Price can only be number";
                return false;
            }

            try
            {
                if (!_mPrice.IsNullOrEmpty() && Convert.ToInt32(_mPrice) <= 0)
                {
                    MPriceError = "Capacity must be larger than 0";
                    return false;
                }
            }
            catch (Exception ex)
            {
                MPriceError = "Invalid price";
                MessageBox.Show(ex.Message);
                return false;
            }

            MPriceError = string.Empty;
            return true;
        }

        private bool ValidateLPrice()
        {
            LPrice = _lPrice.Trim();
            if (Regex.IsMatch(_lPrice, SPECIAL_CHAR_PATTERN) ||
                Regex.IsMatch(_lPrice, SPACE_PATTERN) ||
                Regex.IsMatch(_lPrice, LETTER_PATTERN))
            {
                LPriceError = "Price can only be number";
                return false;
            }

            try
            {
                if (!_lPrice.IsNullOrEmpty() && Convert.ToInt32(_lPrice) <= 0)
                {
                    LPriceError = "Capacity must be larger than 0";
                    return false;
                }
            }
            catch (Exception ex)
            {
                LPriceError = "Invalid price";
                MessageBox.Show(ex.Message);
                return false;
            }

            LPriceError = string.Empty;
            return true;
        }
        #endregion
    }
}