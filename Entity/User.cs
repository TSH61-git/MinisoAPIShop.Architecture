using System;
using System.Collections.Generic;

namespace Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Password { get; set; } = null!;

    public string? Role { get; set; }

    public string? Phone { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
