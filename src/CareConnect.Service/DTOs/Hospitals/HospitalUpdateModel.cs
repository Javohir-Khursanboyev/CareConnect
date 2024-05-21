﻿namespace CareConnect.Service.DTOs.Hospitals;

public class HospitalUpdateModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public long AssetId { get; set; }
}
