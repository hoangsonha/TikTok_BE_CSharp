using System;
using System.Collections.Generic;

namespace TikTokDAOs.Entities;

public partial class Video
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Video1 { get; set; }

    public int? Liked { get; set; }

    public int? Comment { get; set; }

    public int? IdAccount { get; set; }

    public virtual Account? IdAccountNavigation { get; set; }
}
