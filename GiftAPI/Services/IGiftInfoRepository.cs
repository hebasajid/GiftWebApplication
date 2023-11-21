using GiftInfoLibrary.Models;

namespace GiftAPI.Services
{
    public interface IGiftInfoRepository
    {
        Task<IEnumerable<GiftInfo>> SearchGiftsAsync(string category, string name, int? age, string gender, decimal? price);

        Task<bool> AddGiftToFavoritesAsync(int userId, int giftId);

        Task<bool> RemoveGiftFromFavoritesAsync(int userId, int giftId);

        Task<IEnumerable<GiftInfo>> GetFavoriteGiftsAsync(int userId);

        Task<bool> SaveAsync();

        Task AddGiftAsync(GiftInfo gift);

        Task UpdateGiftAsync(GiftInfo gift);

        Task DeleteGiftAsync(int giftId);
        Task<bool> GiftExistsAsync(int giftId);

        Task<IEnumerable<GiftInfo>> GetGiftInfoesAsync();
        Task<GiftInfo> GetGiftByIdAsync(int giftId, bool includeUserInfo);


    }
}
