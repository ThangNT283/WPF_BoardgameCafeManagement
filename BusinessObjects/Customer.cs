using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
