using AutoMapper;
using CareConnect.Data.UnitOfWorks;
using CareConnect.Domain.Entities.DoctorComments;
using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.DoctorComments;
using CareConnect.Service.Exceptions;
using CareConnect.Service.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CareConnect.Service.Services.DoctorComments;

public class DoctorCommentService(
    IMapper mapper,
    IUnitOfWork unitOfWork) : IDoctorCommentService
{
    public async Task<DoctorCommentViewModel> CreateAsync(DoctorCommentCreateModel model)
    {
        var existPatient = await unitOfWork.Patients.SelectAsync(patient => patient.Id == model.PatientId && !patient.IsDeleted)
            ?? throw new NotFoundException($"Patient is not found with this ID={model.PatientId}");

        var existDoctor = await unitOfWork.Doctors.SelectAsync(doctor => doctor.Id == model.DoctorId)
           ?? throw new NotFoundException($"Doctor is not found with this ID={model.DoctorId}");

        var mappedDoctorComment = mapper.Map<DoctorComment>(model);
        mappedDoctorComment.Create();
        var createdDoctorComment = await unitOfWork.DoctorComments.InsertAsync(mappedDoctorComment);
        await unitOfWork.SaveAsync();

        createdDoctorComment.Patient = existPatient;
        createdDoctorComment.Doctor = existDoctor;
        return mapper.Map<DoctorCommentViewModel>(createdDoctorComment);
    }

    public async Task<DoctorCommentViewModel> UpdateAsync(long id, DoctorCommentUpdateModel model)
    {
        var existDoctorComment = await unitOfWork.DoctorComments.SelectAsync(dc => dc.Id == id && !dc.IsDeleted)
            ?? throw new NotFoundException($"Doctor comment is not found with this ID={id}");

        var existPatient = await unitOfWork.Patients.SelectAsync(patient => patient.Id == model.PatientId && !patient.IsDeleted)
           ?? throw new NotFoundException($"Patient is not found with this ID={model.PatientId}");

        var existDoctor = await unitOfWork.Doctors.SelectAsync(doctor => doctor.Id == model.DoctorId)
           ?? throw new NotFoundException($"Doctor is not found with this ID={model.DoctorId}");

        mapper.Map(model, existDoctorComment);
        existDoctorComment.Create();
        var updatedDoctorComment = await unitOfWork.DoctorComments.UpdateAsync(existDoctorComment);
        updatedDoctorComment.Patient = existPatient;
        updatedDoctorComment.Doctor = existDoctor;

        return mapper.Map<DoctorCommentViewModel>(updatedDoctorComment);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existDoctorComment = await unitOfWork.DoctorComments.SelectAsync(dc => dc.Id == id && !dc.IsDeleted)
            ?? throw new NotFoundException($"Doctor comment is not found with this ID={id}");

        existDoctorComment.Create();
        await unitOfWork.DoctorComments.DeleteAsync(existDoctorComment);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<DoctorCommentViewModel> GetByIdAsync(long id)
    {
        var existDoctorComment = await unitOfWork.DoctorComments.SelectAsync(expression: dc => dc.Id == id && !dc.IsDeleted, includes: ["Doctor", "Patient"])
            ?? throw new NotFoundException($"Doctor comment is not found with this ID={id}");

        return mapper.Map<DoctorCommentViewModel>(existDoctorComment);
    }

    public async Task<IEnumerable<DoctorCommentViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var doctorComments = unitOfWork.DoctorComments
            .SelectAsQueryable(expression : dc => !dc.IsDeleted, includes: ["Doctor", "Patient"], isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            doctorComments = doctorComments
                .Where(dc => dc.Comment.ToLower().Contains(search.ToLower()));

        return mapper.Map<IEnumerable<DoctorCommentViewModel>>(await doctorComments.ToPaginateAsQueryable(@params).ToListAsync());
    }
}