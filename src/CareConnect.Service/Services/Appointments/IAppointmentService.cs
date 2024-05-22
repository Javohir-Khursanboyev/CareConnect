using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Appointments;

namespace CareConnect.Service.Services.Appointments;

public interface IAppointmentService
{
    Task<AppointmentViewModel> CreateAsync(AppointmentCreateModel model);
    Task<AppointmentViewModel> UpdateAsync(long id, AppointmentUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<AppointmentViewModel> GetByIdAsync(long id);
    Task<IEnumerable<AppointmentViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
