using AutoMapper;
using CareConnect.Service.Helpers;
using CareConnect.Data.UnitOfWorks;
using CareConnect.Service.Extensions;
using CareConnect.Service.Exceptions;
using CareConnect.Service.DTOs.Assets;
using CareConnect.Domain.Entities.Assets;
using CareConnect.Service.Validators.Assets;

namespace CareConnect.Service.Services.Assets;

public class AssetService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    AssetCreateModelValidator createModelValidator) : IAssetService
{
    public async Task<AssetViewModel> UploadAsync(AssetCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var assetData = await FileHelper.CreateFileAsync(model.File, model.FileType);
        var asset = new Asset()
        {
            Name = assetData.Name,
            Path = assetData.Path,
        };

        asset.Create();
        var createdAsset = await unitOfWork.Assets.InsertAsync(asset);
        await unitOfWork.SaveAsync();

        return mapper.Map<AssetViewModel>(asset);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existAsset = await unitOfWork.Assets.SelectAsync(asset => asset.Id == id)
            ?? throw new NotFoundException("Asset is not found");

        await unitOfWork.Assets.DropAsync(existAsset);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<AssetViewModel> GetByIdAsync(long id)
    {
        var existAsset = await unitOfWork.Assets.SelectAsync(asset => asset.Id == id)
            ?? throw new NotFoundException("Asset is not found");

        return mapper.Map<AssetViewModel>(existAsset);
    }
}