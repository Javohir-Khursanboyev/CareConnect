using CareConnect.Service.DTOs.Assets;

namespace CareConnect.Service.DTOs.Hospitals;

public class HospitalViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public AssetViewModel Asset { get; set; }
}