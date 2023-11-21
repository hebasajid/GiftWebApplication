using GiftAPI.Services;
using GiftInfoLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftInfoAPI.Services
{
    public class GiftInfoRepository : IGiftInfoRepository
    {
        private GiftInfoDbContext _context;

        public GiftInfoRepository(GiftInfoDbContext context)
        {

            _context = context;
        }

        public async Task AddGiftAsync(GiftInfo gift)
        {
            await _context.GiftInfos.AddAsync(gift);
        }

        public async Task UpdateGiftAsync(GiftInfo gift)
        {
            _context.Entry(gift).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GiftInfo>> GetGiftInfoesAsync()
        {
            return await _context.GiftInfos.ToListAsync();
        }


        public async Task DeleteGiftAsync(int giftId)
        {
            var giftToDelete = await _context.GiftInfos.FindAsync(giftId);

            if (giftToDelete != null)
            {
                _context.GiftInfos.Remove(giftToDelete);
                await _context.SaveChangesAsync();
            }
        }



        public async Task<bool> GiftExistsAsync(int giftId)
        {
            return await _context.GiftInfos.AnyAsync<GiftInfo>(g => g.GiftId == giftId);
        }


        public async Task<bool> AddGiftsToFavoritesAsync(int userId, IEnumerable<int> giftIds)
        {
            var user = await _context.UserInfos
                .Include(u => u.FavoriteGifts)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user != null)
            {
                var giftsToAdd = await _context.GiftInfos.Where(g => giftIds.Contains(g.GiftId)).ToListAsync();
                foreach (var giftToAdd in giftsToAdd)
                {
                    user.FavoriteGifts.Add(giftToAdd); // adding gifts to the user's favorite gifts collection
                }
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<UserInfo> GetFavoriteGiftsAsync(int userId)
        {
            return await _context.UserInfos
                .Include(u => u.FavoriteGifts) //getting user info + fave gifts
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<bool> RemoveGiftsFromFavoritesAsync(int userId, IEnumerable<int> giftIds)
        {
            var user = await _context.UserInfos.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user != null)
            {
                var giftsToRemove = user.FavoriteGifts.Where(fg => giftIds.Contains(fg.GiftId)).ToList();
                foreach (var giftToRemove in giftsToRemove)
                {
                    user.FavoriteGifts.Remove(giftToRemove); // removing gifts from the user's favorite gifts collection
                }
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<IEnumerable<GiftInfo>> SearchGiftsAsync(string category, string name, int? age, string gender, decimal? price)
        {
            var query = _context.GiftInfos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(g => g.GiftCategory == category);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(g => g.GiftName.Contains(name));

            if (age.HasValue)
                query = query.Where(g => g.GiftAge == age);

            if (!string.IsNullOrWhiteSpace(gender))
                query = query.Where(g => g.GiftGender == gender);

            if (price.HasValue)
                query = query.Where(g => g.GiftPrice == price);

            return await query.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        //public async Task<IEnumerable<GiftInfo>> GetGiftInfoesAsync()
        //{
        //    return await _context.GiftInfos.ToListAsync();
        //}

        public async Task<GiftInfo> GetGiftByIdAsync(int giftId, bool includeUserInfo)
        {
            if (includeUserInfo)
            {
                return await _context.GiftInfos
                    .Include(g => g.UserInfos)
                    .FirstOrDefaultAsync(g => g.GiftId == giftId);
            }

            return await _context.GiftInfos.FirstOrDefaultAsync(g => g.GiftId == giftId);
        }

        Task<IEnumerable<GiftInfo>> IGiftInfoRepository.GetFavoriteGiftsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<GiftInfo>> IGiftInfoRepository.SearchGiftsAsync(string category, string name, int? age, string gender, decimal? price)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddGiftToFavoritesAsync(int userId, int giftId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveGiftFromFavoritesAsync(int userId, int giftId)
        {
            throw new NotImplementedException();
        }

        //Task<IEnumerable<GiftInfo>> IGiftInfoRepository.GetFavoriteGiftsAsync(int userId)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IEnumerable<GiftInfo>> IGiftInfoRepository.GetGiftInfoesAsync()
        //{
        //    throw new NotImplementedException();
        //}

        Task<GiftInfo> IGiftInfoRepository.GetGiftByIdAsync(int giftId, bool includeUserInfo)
        {
            throw new NotImplementedException();
        }

        //Task<IEnumerable<GiftInfo>> IGiftInfoRepository.SearchGiftsAsync(string category, string name, int? age, string gender, decimal? price)
        //{
        //    throw new NotImplementedException();
        //}



        //Task<IEnumerable<GiftInfo>> IGiftInfoRepository.GetGiftInfoesAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //Task<GiftInfo> IGiftInfoRepository.GetGiftByIdAsync(int giftId, bool includeUserInfo)
        //{
        //    throw new NotImplementedException();
        //}






    }
}