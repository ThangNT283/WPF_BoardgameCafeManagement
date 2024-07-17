namespace BusinessObjects.Builders
{
    public class BillDetailBuilder : IBillDetailBuilder
    {
        private BillDetail _billDetail;

        public BillDetailBuilder()
        {
            _billDetail = new BillDetail();
        }

        public IBillDetailBuilder SetId(int id)
        {
            _billDetail.Id = id;
            return this;
        }
        public IBillDetailBuilder SetBillId(int billId)
        {
            _billDetail.BillId = billId;
            return this;
        }
        public IBillDetailBuilder SetDrinkVariationId(int drinkVariationId)
        {
            _billDetail.DrinkVariationId = drinkVariationId;
            return this;
        }
        public IBillDetailBuilder SetQuantity(int quantity)
        {
            _billDetail.Quantity = quantity;
            return this;
        }
        public BillDetail Build() => _billDetail;
    }
}
