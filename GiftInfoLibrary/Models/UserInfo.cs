using System;
using System.Collections.Generic;

namespace GiftInfoLibrary.Models;

public partial class UserInfo
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserPass { get; set; } = null!;

    public int? FavoriteGiftId { get; set; }

    public virtual GiftInfo FavoriteGift { get; set; }

    public virtual ICollection<GiftInfo> FavoriteGifts { get; set; } = new List<GiftInfo>();
}
