using RedisAsPrimaryDatabase.Models;

namespace RedisAsPrimaryDatabase.Data;

public interface IPlatformRepositry
{
    Task<IList<Platfrom>> GetAll();
    Task<Platfrom> GetById(string id);
    Task Create(Platfrom platfrom);
    Task Update(Platfrom platfrom);
    Task Delete(string id);
}