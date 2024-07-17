using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Bill
{
    public int Id { get; set; }

    public int TableId { get; set; }

    public string? CustomerName { get; set; }

    public int NumberOfGames { get; set; }

    public DateTime PaidAt { get; set; }

    public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

    public virtual Table Table { get; set; } = null!;
}
