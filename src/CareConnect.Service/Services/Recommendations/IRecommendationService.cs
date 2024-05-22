using CareConnect.Domain.Entities.Recommendations;
using CareConnect.Service.Configurations;

namespace CareConnect.Service.Services.Recommendations;

public interface IRecommendationService
{
    Task<RecommendationViewModel> CreateAsync(RecommendationsCreateModel model);
    Task<bool> DeleteAsync(long id);
    Task<RecommendationViewModel> GetByIdAsync(long id);
    Task<IEnumerable<RecommendationViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}