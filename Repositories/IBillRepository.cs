﻿using BusinessObjects;

namespace Repositories
{
    public interface IBillRepository
    {
        List<Bill> GetBills();
        bool CreateBill(Bill bill);
        bool UpdateBill(Bill bill);
        bool DeleteBill(int billId);
        List<Bill> SearchBill(int? tableId, int? customerId, DateTime? startTime, DateTime? endTime);
    }
}
