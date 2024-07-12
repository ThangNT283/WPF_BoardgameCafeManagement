using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class BillDetail
{
    public int Id { get; set; }

    public int BillId { get; set; }

    public int DrinkVariationId { get; set; }

    public int Quantity { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual DrinkVariation DrinkVariation { get; set; } = null!;
}
