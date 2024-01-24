using PlatformService.Models;

namespace PlatformService.Data;

public interface IPlatformRepo
{
    Task<bool> SaveChangesAsync();

    Task<IEnumerable<Platform>> GetAllPlatforms();

    Task<Platform?> GetPlatformById(int id);

    Task CreatePlatform(Platform platform);
    Task UpdatePlatform(int id, Platform platform);
    Task DeletePlatform(int id);
}
