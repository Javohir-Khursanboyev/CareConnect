using AutoMapper;
using CareConnect.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using CareConnect.Service.Exceptions;
using CareConnect.Service.Extensions;
using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.DoctorStars;
using CareConnect.Domain.Entities.DoctorStars;
using CareConnect.Service.Validators.DoctorStars;

namespace CareConnect.Service.Services.DoctorStars;

public class DoctorStarService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    DoctorStarCreateModelValidator createModelValidator) : IDoctorStarService
{
    public async Task<DoctorStarViewModel> CreateAsync(DoctorStarCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var existDoctor = await unitOfWork.Doctors.SelectAsync(d => d.Id == model.DoctorId && !d.IsDeleted)
            ?? throw new NotFoundException("Doctor is not found");
        var existPatient = await unitOfWork.Patients.SelectAsync(p => p.Id == model.PatientId && !p.IsDeleted)
            ?? throw new NotFoundException("Patient is not found");

        var existDoctorStar = await unitOfWork.DoctorStars.
            SelectAsync(ds => ds.DoctorId == model.DoctorId && ds.PatientId == model.PatientId);

        if (existDoctor is not null)
            throw new AlreadyExistException("Doctor star is already exist");

        var doctorStar = mapper.Map<DoctorStar>(model);
        doctorStar.Create();
        doctorStar.Doctor = existDoctor;
        doctorStar.Patient = existPatient;

        var createdDoctorStar = await unitOfWork.DoctorStars.InsertAsync(doctorStar);
        await unitOfWork.SaveAsync();

        return mapper.Map<DoctorStarViewModel>(createdDoctorStar);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existDoctorStar = await unitOfWork.DoctorStars.SelectAsync(ds => ds.Id == id)
            ?? throw new NotFoundException("Doctor star is not found");

        await unitOfWork.DoctorStars.DeleteAsync(existDoctorStar);
        existDoctorStar.Delete();
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<DoctorStarViewModel> GetByIdAsync(long id)
    {
        var existDoctorStar = await unitOfWork.DoctorStars.
            SelectAsync(expression: ds => ds.Id == id && !ds.IsDeleted, includes: ["Doctor", "Patient"])
            ?? throw new NotFoundException("Doctor Star is not found");

        return mapper.Map<DoctorStarViewModel>(existDoctorStar);
    }

    public async Task<IEnumerable<DoctorStarViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var doctorStars = unitOfWork.DoctorStars.
            SelectAsQueryable(expression: ds => !ds.IsDeleted, includes: ["Doctor", "Patient"], isTracked: false).
            OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            doctorStars = doctorStars.Where(ds =>
            ds.Doctor.User.FirstName.ToLower().Contains(search.ToLower()) ||
            ds.Patient.User.FirstName.ToLower().Contains(search.ToLower()) ||
            ds.Patient.User.LastName.ToLower().Contains(search.ToLower()));

        var paginateDoctorStar = await doctorStars.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<DoctorStarViewModel>>(paginateDoctorStar);
    }
}
