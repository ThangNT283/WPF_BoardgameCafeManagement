﻿using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class GameType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
