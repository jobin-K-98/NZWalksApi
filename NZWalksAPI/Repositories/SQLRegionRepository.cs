using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var exisitingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (exisitingRegion == null) 
                return null;
            dbContext.Regions.Remove(exisitingRegion);
            await dbContext.SaveChangesAsync();
            return exisitingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
           return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
           return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var exisitingRegion = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (exisitingRegion == null)
                return null;

            exisitingRegion.Code = region.Code;
            exisitingRegion.Name = region.Name;
            exisitingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return exisitingRegion;
        }
    }
}
