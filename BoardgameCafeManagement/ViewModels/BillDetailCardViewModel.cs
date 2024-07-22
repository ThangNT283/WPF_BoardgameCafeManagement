using BusinessObjects;
using Repositories;
using Services;
using System.Collections.ObjectModel;

namespace BoardgameCafeManagement.ViewModels
{
    public class BillDetailCardViewModel : ViewModelBase
    {
        private readonly ITableService _tableService;
        private readonly IBillDetailService _billDetailService;
        private readonly IDrinkVariationService _drinkVariationService;
        private readonly IDrinkService _drinkService;

        #region Fields
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
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
        private int _gamePlayed;
        public int GamePlayed
        {
            get { return _gamePlayed; }
            set { _gamePlayed = value; OnPropertyChanged(nameof(GamePlayed)); }
        }
        private DateTime _paidAt;
        public DateTime PaidAt
        {
            get { return _paidAt; }
            set { _paidAt = value; OnPropertyChanged(nameof(PaidAt)); }
        }
        private ObservableCollection<BillDetail> _billDetails;
        public ObservableCollection<BillDetail> BillDetails
        {
            get { return _billDetails; }
            set { _billDetails = value; OnPropertyChanged(nameof(BillDetails)); }
        }
        private int _total;
        public int Total
        {
            get { return _total; }
            set { _total = value; OnPropertyChanged(nameof(Total)); }
        }
        private int _gameTotal;
        public int GameTotal
        {
            get { return _gameTotal; }
            set { _gameTotal = value; OnPropertyChanged(nameof(GameTotal)); }
        }
        private int _drinkTotal;
        public int DrinkTotal
        {
            get { return _drinkTotal; }
            set { _drinkTotal = value; OnPropertyChanged(nameof(DrinkTotal)); }
        }
        #endregion

        public BillDetailCardViewModel(Bill selectedBill)
        {
            _tableService = new TableService();
            _billDetailService = new BillDetailService();
            _drinkVariationService = new DrinkVariationService();
            _drinkService = new DrinkService();

            if (selectedBill != null)
            {
                Id = selectedBill.Id;
                TableNumber = _tableService.GetTables().FirstOrDefault(t => t.Id == selectedBill.TableId).TableNumber;
                CustomerName = selectedBill.CustomerName;
                GamePlayed = selectedBill.NumberOfGames;
                PaidAt = selectedBill.PaidAt;

                BillDetails = _billDetailService.GetDetailByBillId(Id);
                foreach (BillDetail billDetail in BillDetails)
                {
                    billDetail.DrinkVariation = _drinkVariationService.GetVariationById(billDetail.DrinkVariationId);
                    billDetail.DrinkVariation.Drink =
                        _drinkService.GetDrinks().FirstOrDefault(d => d.Id == billDetail.DrinkVariation.DrinkId);
                    DrinkTotal += billDetail.DrinkVariation.Price * billDetail.Quantity;
                }
                GameTotal += selectedBill.NumberOfGames * 5;
                Total = DrinkTotal + GameTotal;
            }
        }
    }
}
