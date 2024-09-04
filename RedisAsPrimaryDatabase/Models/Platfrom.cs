namespace RedisAsPrimaryDatabase.Models;

public class Platfrom
{
    public string Id { get; set; } = $"platform:{Guid.NewGuid()}";
    public string Name { get; set; } = default!;
}