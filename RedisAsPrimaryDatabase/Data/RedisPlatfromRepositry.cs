using System.Text.Json;
using RedisAsPrimaryDatabase.Models;
using StackExchange.Redis;

namespace RedisAsPrimaryDatabase.Data;

public class RedisPlatfromRepositry(IConnectionMultiplexer connectionMultiplexer): IPlatformRepositry
{
    private readonly string hashSetName = "hashplatform";
    public async Task<IList<Platfrom>> GetAll()
    {
        var db = connectionMultiplexer.GetDatabase();
        var allPlatForms = db.HashGetAll(hashSetName);
        if (allPlatForms.Length > 0)
        {
            var obj = Array.ConvertAll(allPlatForms, i => JsonSerializer.Deserialize<Platfrom>(i.Value)).ToList();
            return obj;
        }

        return null;
    }

    public async Task<Platfrom> GetById(string id)
    {
        var db = connectionMultiplexer.GetDatabase();
        var platform = await db.HashGetAsync(hashSetName, id);
        if (!string.IsNullOrEmpty(platform))
        {
            return JsonSerializer.Deserialize<Platfrom>(platform.ToString());
        }

        return null;
    }

    public async Task Create(Platfrom platfrom)
    {
        if (platfrom is null)
        {
            throw new ArgumentNullException(nameof(platfrom));
        }

        var db = connectionMultiplexer.GetDatabase();
        await db.HashSetAsync(hashSetName, new HashEntry[] { new HashEntry(platfrom.Id, JsonSerializer.Serialize(platfrom)) });
    }

    public async Task Update(Platfrom platfrom)
    {
        var db = connectionMultiplexer.GetDatabase();
        await db.HashSetAsync(hashSetName, new HashEntry[] { new HashEntry(platfrom.Id, JsonSerializer.Serialize(platfrom)) });
    }

    public async Task Delete(string id)
    {
        var platform = await GetById(id);
        if (platform is null)
        {
            throw new ArgumentNullException();
        }

        var db = connectionMultiplexer.GetDatabase();
        db.HashDelete(hashSetName, platform.Id);
    }
}