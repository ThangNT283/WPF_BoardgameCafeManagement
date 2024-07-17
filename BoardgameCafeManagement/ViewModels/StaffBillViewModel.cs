using BoardgameCafeManagement.Commands;
using BusinessObjects;
using BusinessObjects.Builders;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace BoardgameCafeManagement.ViewModels
{
    public class StaffBillViewModel : ViewModelBase
    {
        private readonly IDrinkService _drinkService;
        private readonly IDrinkVariationService _drinkVariationService;
        private readonly ITableService _tableService;
        private readonly IBillService _billService;
        private readonly IBillDetailService _billDetailService;

        private static string SPECIAL_CHAR_PATTERN = @"[!@#$%^&*(),.?""':{}|<>]";
        private static string NUMBER_PATTERN = @"[0-9]";
        private static string LETTER_PATTERN = @"[a-zA-Z]";
        private static string SPACE_PATTERN = @"\s";

        #region Fields
        private ObservableCollection<Drink> _drinkTables;
        public ObservableCollection<Drink> DrinkTables
        {
            get { return _drinkTables; }
            set { _drinkTables = value; OnPropertyChanged(nameof(DrinkTables)); }
        }
        private Drink? _selectedDrink;
        public Drink? SelectedDrink
        {
            get { return _selectedDrink; }
            set { _selectedDrink = value; OnPropertyChanged(nameof(SelectedDrink)); }
        }
        private string _quantity;
        public string Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }
        private string _size;
        public string Size
        {
            get { return _size; }
            set { _size = value; OnPropertyChanged(nameof(Size)); }
        }
        private ObservableCollection<OrderItem> _orderItems;
        public ObservableCollection<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set { _orderItems = value; OnPropertyChanged(nameof(OrderItems)); }
        }
        private OrderItem _selectedItem;
        public OrderItem SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }
        private ObservableCollection<Table> _inUsedTables;
        public ObservableCollection<Table> InUsedTables
        {
            get { return _inUsedTables; }
            set { _inUsedTables = value; OnPropertyChanged(nameof(InUsedTables)); }
        }
        private string _tableNumber;
        public string TableNumber
        {
            get { return _tableNumber; }
            set { _tableNumber = value; OnPropertyChanged(nameof(TableNumber)); }
        }
        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; OnPropertyChanged(nameof(CustomerName)); }
        }
        private string _playedGames;
        public string PlayedGames
        {
            get { return _playedGames; }
            set { _playedGames = value; OnPropertyChanged(nameof(PlayedGames)); }
        }
        private string _tableNumberError;
        public string TableNumberError
        {
            get { return _tableNumberError; }
            set { _tableNumberError = value; OnPropertyChanged(nameof(TableNumberError)); }
        }
        private string _customerNameError;
        public string CustomerNameError
        {
            get { return _customerNameError; }
            set { _customerNameError = value; OnPropertyChanged(nameof(CustomerNameError)); }
        }
        private string _playedGamesError;
        public string PlayedGamesError
        {
            get { return _playedGamesError; }
            set { _playedGamesError = value; OnPropertyChanged(nameof(PlayedGamesError)); }
        }
        private string _searchInput;
        public string SearchInput
        {
            get { return _searchInput; }
            set { _searchInput = value; OnPropertyChanged(nameof(SearchInput)); }
        }
        private ObservableCollection<DrinkVariation> _variationOption;
        public ObservableCollection<DrinkVariation> VariationOptions
        {
            get { return _variationOption; }
            set { _variationOption = value; OnPropertyChanged(nameof(VariationOptions)); }
        }
        #endregion

        #region Commands
        public RelayCommand AddItem { get; }
        public RelayCommand DeleteItem { get; }
        public RelayCommand SubmitOrder { get; }
        public RelayCommand SearchDrink { get; }
        public RelayCommand DrinkSelectionChanged { get; }

        #endregion

        public StaffBillViewModel()
        {
            _drinkService = new DrinkService();
            _drinkVariationService = new DrinkVariationService();
            _tableService = new TableService();
            _billService = new BillService();
            _billDetailService = new BillDetailService();

            DrinkTables = _drinkService.GetDrinks();
            OrderItems = new ObservableCollection<OrderItem>();
            InUsedTables = _tableService.GetInUsedTables();
            VariationOptions = new ObservableCollection<DrinkVariation>();

            AddItem = new RelayCommand(AddToOrder);
            DeleteItem = new RelayCommand(DeleteFromOrder);
            SubmitOrder = new RelayCommand(PlaceOrder);
            SearchDrink = new RelayCommand(SearchForDrink);
            DrinkSelectionChanged = new RelayCommand(UpdateVariationOptions);
        }

        #region Actions
        private void AddToOrder()
        {
            try
            {
                if (IsValidItemFields())
                {
                    OrderItem? foundDuplicateItem = OrderItems.FirstOrDefault(i => i.DrinkName == SelectedDrink.Name && i.DrinkSize == Size);
                    if (foundDuplicateItem != null)
                    {
                        foundDuplicateItem.Quantity += Convert.ToInt32(Quantity);
                        ClearItemFields();
                        return;
                    }

                    OrderItem newItem = new OrderItem();
                    newItem.DrinkName = SelectedDrink.Name;
                    newItem.DrinkSize = Size;
                    newItem.Price = _drinkVariationService.GetPriceByVariation(SelectedDrink.Id, Size) * 1000;
                    newItem.Quantity = Convert.ToInt32(Quantity);
                    OrderItems.Add(newItem);

                    ClearItemFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteFromOrder()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Choose an item in the order infomration board to delete");
                return;
            }

            OrderItems.Remove(SelectedItem);
        }

        private void PlaceOrder()
        {
            try
            {
                if (IsValidOrderFields())
                {
                    IBillBuilder billBuilder = new BillBuilder();
                    bool isSuccess = _billService.CreateBill(billBuilder
                        .SetTableId(_tableService.GetTables().FirstOrDefault(t => t.TableNumber == TableNumber).Id)
                        .SetCustomerName(CustomerName)
                        .SetNumberOfGames(Convert.ToInt32(PlayedGames))
                        .Build());
                    if (isSuccess)
                    {
                        int billId = _tableService.GetTables().OrderByDescending(i => i.Id).FirstOrDefault().Id;
                        foreach (OrderItem item in OrderItems)
                        {
                            int variationId = 0;
                            Drink drink = _drinkService.GetDrinkByName(item.DrinkName);
                            if (drink != null)
                            {
                                variationId = _drinkVariationService.GetVariationsByDrinkId(drink.Id)
                                    .FirstOrDefault(v => v.VariationName == Size).Id;
                            }
                            else
                            {
                                MessageBox.Show("Drink not found");
                            }

                            IBillDetailBuilder billDetailBuilder = new BillDetailBuilder();
                            isSuccess &= _billDetailService.CreateBillDetail(billDetailBuilder
                                .SetBillId(billId)
                                .SetDrinkVariationId(variationId)
                                .SetQuantity(item.Quantity)
                                .Build());
                        }

                        if (isSuccess)
                        {
                            MessageBox.Show("Bill is created successfully!");
                            ClearOrderFields();
                            ClearItemFields();
                        }
                        else
                        {
                            MessageBox.Show("Failed to create bill detail");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to create bill");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchForDrink()
        {
            if (SearchInput.IsNullOrEmpty())
            {
                DrinkTables = _drinkService.GetDrinks();
            }
            else
            {
                SearchInput = Regex.Replace(SearchInput.Trim(), @"\s+", " ");
                DrinkTables = _drinkService.SearchDrink(SearchInput);
            }
        }

        private void UpdateVariationOptions()
        {
            if (SelectedDrink != null)
            {
                VariationOptions = _drinkVariationService.GetVariationsByDrinkId(SelectedDrink.Id);
                Size = VariationOptions[0].VariationName;
                Quantity = "1";
            }
        }

        private void ClearItemFields()
        {
            Quantity = "";
            Size = "";
            SelectedDrink = null;
        }

        private void ClearOrderFields()
        {
            TableNumber = "";
            CustomerName = "";
            Quantity = "";
        }
        #endregion

        #region Validation
        private bool IsValidItemFields()
        {
            bool res = true;

            res &= IsValidQuantity();
            res &= IsDrinkSelected();
            res &= IsValidSize();

            return res;
        }

        private bool IsValidQuantity()
        {
            if (_quantity.IsNullOrEmpty())
            {
                MessageBox.Show("Quantity cannot be blank");
                return false;
            }

            Quantity = _quantity.Trim();
            if (Regex.IsMatch(_quantity, SPECIAL_CHAR_PATTERN) ||
                Regex.IsMatch(_quantity, SPACE_PATTERN) ||
                Regex.IsMatch(_quantity, LETTER_PATTERN))
            {
                MessageBox.Show("Quantity can only be number");
                return false;
            }

            try
            {
                if (Convert.ToInt32(_quantity) <= 0)
                {
                    MessageBox.Show("Quantity must be larger than 0");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid quantity: " + ex.Message);
                return false;
            }

            return true;
        }

        private bool IsDrinkSelected()
        {
            if (SelectedDrink == null)
            {
                MessageBox.Show("Choose a drink before add to order");
                return false;
            }
            return true;
        }

        private bool IsValidSize()
        {
            if (_quantity.IsNullOrEmpty())
            {
                MessageBox.Show("Size cannot be blank");
                return false;
            }

            if (_drinkVariationService.GetPriceByVariation(SelectedDrink.Id, Size) < 0)
            {
                MessageBox.Show("This drink does not have " + Size);
                return false;
            }
            return true;
        }

        private bool IsValidOrderFields()
        {
            bool res = true;

            res &= IsValidTableNumber();
            res &= IsValidCustomerName();
            res &= IsValidPlayedGames();

            return res;

        }

        private bool IsValidTableNumber()
        {
            if (_tableNumber.IsNullOrEmpty())
            {
                TableNumberError = "Table number cannot be empty";
                return false;
            }

            CustomerNameError = string.Empty;
            return true;
        }

        private bool IsValidCustomerName()
        {
            if (_customerName.IsNullOrEmpty())
            {
                CustomerNameError = "Customer name cannot be empty";
                return false;
            }

            CustomerName = Regex.Replace(_customerName.Trim(), @"\s+", " ");
            if (Regex.IsMatch(_customerName, SPECIAL_CHAR_PATTERN) || Regex.IsMatch(_customerName, NUMBER_PATTERN))
            {
                CustomerNameError = "No special character or number in customer name";
                return false;
            }
            if (_customerName.Length > 128)
            {
                CustomerNameError = "Customer name is too long";
                return false;
            }

            CustomerNameError = string.Empty;
            return true;
        }

        private bool IsValidPlayedGames()
        {
            if (_playedGames.IsNullOrEmpty())
            {
                PlayedGamesError = "Played games cannot be blank";
                return false;
            }

            PlayedGames = _playedGames.Trim();
            if (Regex.IsMatch(_playedGames, SPECIAL_CHAR_PATTERN) ||
                Regex.IsMatch(_playedGames, SPACE_PATTERN) ||
                Regex.IsMatch(_playedGames, LETTER_PATTERN))
            {
                PlayedGamesError = "Played games can only be number";
                return false;
            }

            try
            {
                if (Convert.ToInt32(_playedGames) <= 0)
                {
                    PlayedGamesError = "Played games must be larger than 0";
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid played games: " + ex.Message);
                return false;
            }

            PlayedGamesError = string.Empty;
            return true;
        }
        #endregion
    }

    public class OrderItem : INotifyPropertyChanged
    {
        private string _drinkName;
        private string _drinkSize;
        private int _quantity;
        private int _price;

        public string DrinkName
        {
            get { return _drinkName; }
            set
            {
                if (_drinkName != value)
                {
                    _drinkName = value;
                    OnPropertyChanged(nameof(DrinkName));
                }
            }
        }

        public string DrinkSize
        {
            get { return _drinkSize; }
            set
            {
                if (_drinkSize != value)
                {
                    _drinkSize = value;
                    OnPropertyChanged(nameof(DrinkSize));
                }
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                    OnPropertyChanged(nameof(SubTotal));
                }
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                    OnPropertyChanged(nameof(SubTotal));
                }
            }
        }

        public decimal SubTotal
        {
            get { return _quantity * _price; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
