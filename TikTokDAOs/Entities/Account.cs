using System;
using System.Collections.Generic;

namespace TikTokDAOs.Entities;

public partial class Account
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? Liked { get; set; }

    public int? Followed { get; set; }

    public string? Avatar { get; set; }

    public string? Contact { get; set; }

    public string? NickName { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
