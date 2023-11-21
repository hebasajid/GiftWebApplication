﻿using System;
using System.Collections.Generic;

namespace GiftInfoLibrary.Models;

public partial class GiftInfo
{
    public int GiftId { get; set; }

    public string GiftName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal GiftPrice { get; set; }

    public int? GiftAge { get; set; }

    public string? GiftCategory { get; set; }

    public string? GiftGender { get; set; }

    public string? GiftUrl { get; set; }

    public virtual ICollection<UserInfo> UserInfos { get; set; } = new List<UserInfo>();
}
