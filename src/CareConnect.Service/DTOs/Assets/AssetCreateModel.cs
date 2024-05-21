using CareConnect.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace CareConnect.Service.DTOs.Assets;

public class AssetCreateModel
{
    public IFormFile File { get; set; }
    public FileType FileType { get; set; }
}