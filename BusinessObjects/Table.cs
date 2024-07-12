using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Table
{
    public int Id { get; set; }

    public string TableNumber { get; set; } = null!;

    public int Capacity { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
