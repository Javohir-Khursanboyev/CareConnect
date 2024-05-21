using CareConnect.Domain.Commons;

namespace CareConnect.Domain.Entities.Assets;

public class Asset : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
}
