namespace BusinessObjects.Builders
{
    public interface IBillDetailBuilder
    {
        IBillDetailBuilder SetId(int id);
        IBillDetailBuilder SetBillId(int billId);
        IBillDetailBuilder SetDrinkVariationId(int drinkVariationId);
        IBillDetailBuilder SetQuantity(int quantity);
        BillDetail Build();
    }
}
