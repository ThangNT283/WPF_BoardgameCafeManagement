using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class DrinkVariation
{
    public int Id { get; set; }

    public int DrinkId { get; set; }

    public string VariationName { get; set; } = null!;

    public int Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

    public virtual Drink Drink { get; set; } = null!;
}
