using AutoMapper;
using CareConnect.Data.UnitOfWorks;
using CareConnect.Service.Exceptions;
using CareConnect.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using CareConnect.Service.Configurations;
using CareConnect.Domain.Entities.Recommendations;
using CareConnect.Service.Validators.Recommendations;

namespace CareConnect.Service.Services.Recommendations;

public class RecommendationService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    RecommendationCreateModelValidator createModelValidator) : IRecommendationService
{
    public async Task<RecommendationViewModel> CreateAsync(RecommendationsCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var existAppointment = await unitOfWork.Appointments.SelectAsync(appointment => appointment.Id == model.AppointmentId && !appointment.IsDeleted)
            ?? throw new NotFoundException($"This appointment is not found with this ID={model.AppointmentId}");

        var mappedRecommendation = mapper.Map<Recommendation>(model);
        mappedRecommendation.Create();

        var createdRecommendation = await unitOfWork.Recommendations.InsertAsync(mappedRecommendation);
        await unitOfWork.SaveAsync();

        mappedRecommendation.Appointment = existAppointment;
        return mapper.Map<RecommendationViewModel>(createdRecommendation);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existRecommendation = await unitOfWork.Recommendations.SelectAsync(r => r.Id == id && !r.IsDeleted)
            ?? throw new NotFoundException($"This recommendation is not found with this ID={id}");

        existRecommendation.Delete();
        await unitOfWork.Recommendations.DeleteAsync(existRecommendation);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<RecommendationViewModel> GetByIdAsync(long id)
    {
        var existRecommendation = await unitOfWork.Recommendations
            .SelectAsync(expression: r => r.Id == id && !r.IsDeleted, includes: ["Appointment"])
            ?? throw new NotFoundException($"This recommendation is not found with this ID={id}");

        return mapper.Map<RecommendationViewModel>(existRecommendation);
    }

    public async Task<IEnumerable<RecommendationViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var recommendations = unitOfWork.Recommendations
            .SelectAsQueryable(expression: r => !r.IsDeleted, includes: ["Appointment.Patient.User"], isTracked : false) 
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            recommendations
                .Where(r => r.Appointment.Patient.User.FirstName.ToLower().Contains(search.ToLower()) ||
                r.Appointment.Patient.User.LastName.ToLower().Contains(search.ToLower()));

        return mapper.Map<IEnumerable<RecommendationViewModel>>(await recommendations.ToPaginateAsQueryable(@params).ToListAsync());
    }
}