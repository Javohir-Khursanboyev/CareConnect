using CareConnect.Domain.Commons;
using CareConnect.Domain.Entities.Assets;
using CareConnect.Domain.Entities.Departments;

namespace CareConnect.Domain.Entities.Hospitals;

public class Hospital : Auditable
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public long AssetId { get; set; }

    public Asset Asset { get; set; }

    public IEnumerable<Department> Departments { get; set; }
}
