using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Game
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int MinPlayerNumber { get; set; }

    public int MaxPlayerNumber { get; set; }

    public int TypeId { get; set; }

    public bool Status { get; set; }

    public virtual GameType Type { get; set; } = null!;
}
