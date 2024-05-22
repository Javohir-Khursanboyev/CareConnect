using AutoMapper;
using CareConnect.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using CareConnect.Service.Exceptions;
using CareConnect.Service.Extensions;
using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Appointments;
using CareConnect.Domain.Entities.Appointments;

namespace CareConnect.Service.Services.Appointments;

public class AppointmentService(
    IMapper mapper,
    IUnitOfWork unitOfWork) : IAppointmentService
{
    public async Task<AppointmentViewModel> CreateAsync(AppointmentCreateModel model)
    {
        var existDoctor = await unitOfWork.Doctors.SelectAsync(d => d.Id == model.DoctorId && !d.IsDeleted)
            ?? throw new NotFoundException("Doctor is not found");
        var existPatient = await unitOfWork.Patients.SelectAsync(p => p.Id == model.PatientId)
            ?? throw new NotFoundException("Patient is not found");

        var existAppointment = await unitOfWork.Appointments.
            SelectAsync(a => a.DoctorId == model.DoctorId && a.PatientId == model.PatientId && a.Date == model.Date);

        if (existAppointment is not null)
            throw new AlreadyExistException("Appointment is already exist");

        var appointment = mapper.Map<Appointment>(model);
        appointment.Create();
        appointment.Doctor = existDoctor;
        appointment.Patient = existPatient;

        var createdAppointment = await unitOfWork.Appointments.InsertAsync(appointment);
        await unitOfWork.SaveAsync();

        return mapper.Map<AppointmentViewModel>(createdAppointment);
    }

    public async Task<AppointmentViewModel> UpdateAsync(long id, AppointmentUpdateModel model)
    {
        var existAppointment = await unitOfWork.Appointments.SelectAsync(a => a.Id == id)
            ?? throw new NotFoundException("Appointment is not found");

        var existDoctor = await unitOfWork.Doctors.SelectAsync(d => d.Id == model.DoctorId && !d.IsDeleted)
            ?? throw new NotFoundException("Doctor is not found");
        var existPatient = await unitOfWork.Patients.SelectAsync(p => p.Id == model.PatientId)
            ?? throw new NotFoundException("Patient is not found");

        var alreadyAppointment = await unitOfWork.Appointments.
           SelectAsync(a => a.DoctorId == model.DoctorId && a.PatientId == model.PatientId && a.Date == model.Date);

        if (alreadyAppointment is not null)
            throw new AlreadyExistException("Appointment is already exist");

        mapper.Map(model, existAppointment);
        existAppointment.Update();
        existAppointment.Doctor = existDoctor;
        existAppointment.Patient = existPatient;

        var updatedAppointment = await unitOfWork.Appointments.UpdateAsync(existAppointment);
        await unitOfWork.SaveAsync();

        return mapper.Map<AppointmentViewModel>(updatedAppointment);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existAppointment = await unitOfWork.Appointments.SelectAsync(a => a.Id == id)
            ?? throw new NotFoundException("Appointment is not found");

        await unitOfWork.Appointments.DeleteAsync(existAppointment);
        existAppointment.Delete();
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<AppointmentViewModel> GetByIdAsync(long id)
    {
        var existAppointment = await unitOfWork.Appointments.
            SelectAsync(expression: a => a.Id == id && !a.IsDeleted, includes: ["Doctor", "Patient"])
            ?? throw new NotFoundException("Appointment is not found");

        return mapper.Map<AppointmentViewModel>(existAppointment);
    }

    public async Task<IEnumerable<AppointmentViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var appointments = unitOfWork.Appointments.
            SelectAsQueryable(expression: a => !a.IsDeleted, includes: ["Doctor", "Patient"], isTracked: false).
            OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            appointments = appointments.Where(rp =>
            rp.Doctor.User.FirstName.ToLower().Contains(search.ToLower()) ||
            rp.Patient.User.FirstName.ToLower().Contains(search.ToLower()));

        var paginateAppointments = await appointments.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<AppointmentViewModel>>(paginateAppointments);
    }
}
