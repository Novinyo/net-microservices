using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

public class PlatformRepo : IPlatformRepo
{
    private readonly AppDbContext _context;

    public PlatformRepo(AppDbContext context)
    {
        _context = context;
    }
    public async Task CreatePlatform(Platform platform)
    {
        if (platform == null) throw new ArgumentNullException(nameof(platform));
        await _context.Platforms.AddAsync(platform);
    }

    public async Task DeletePlatform(int id)
    {
        var platform = await GetPlatform(id);

        if(platform != null)
        {
            _context.Platforms.Remove(platform);
        }
    }

    public async Task UpdatePlatform(int id, Platform platform)
    {
        var existing = await GetPlatform(id);

        if(existing != null)
        {
            existing.Name = platform.Name;
            existing.Publisher = platform.Publisher;
            existing.Cost = platform.Cost;

            _context.Platforms.Update(existing);
        }        
    }

    public async Task<IEnumerable<Platform>> GetAllPlatforms()
    {
        return await _context.Platforms.ToListAsync();
    }

    public async Task<Platform?> GetPlatformById(int id)
    {
        return await GetPlatform(id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<Platform?> GetPlatform(int id)
    {
        return await _context.Platforms.FindAsync(id);
    }
}
