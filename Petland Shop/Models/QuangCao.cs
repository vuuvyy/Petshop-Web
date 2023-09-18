﻿using System;
using System.Collections.Generic;

namespace Petland_Shop.Models;

public partial class QuangCao
{
    public int QuangCaoId { get; set; }

    public string? SubTitle { get; set; }

    public string? Title { get; set; }

    public string? ImageBg { get; set; }

    public string? ImageProduct { get; set; }

    public string? UrlLink { get; set; }

    public bool Active { get; set; }

    public DateTime? CreateDate { get; set; }
}
