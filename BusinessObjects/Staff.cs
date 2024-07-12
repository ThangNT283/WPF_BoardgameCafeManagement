using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Staff
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public bool Status { get; set; }
}
