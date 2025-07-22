using System;
using System.Collections.Generic;

namespace GraphQLDemoApi.Models;

public partial class UserLogin
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? Parent { get; set; }

    public string? Email { get; set; }

    public string? Name { get; set; }

    public string? CounselorCode { get; set; }

    public int? NumberOfEcontracts { get; set; }

    public string? UserKeyHash { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? IsWaived { get; set; }

    public int? OneTimeCode { get; set; }
}
