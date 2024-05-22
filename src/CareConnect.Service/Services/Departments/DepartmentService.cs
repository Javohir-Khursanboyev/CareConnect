using AutoMapper;
using CareConnect.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using CareConnect.Service.Exceptions;
using CareConnect.Service.Extensions;
using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Departments;
using CareConnect.Domain.Entities.Departments;

namespace CareConnect.Service.Services.Departments;

public class DepartmentService(
    IMapper mapper,
    IUnitOfWork unitOfWork) : IDepartmentService
{
    public async Task<DepartmentViewModel> CreateAsync(DepartmentCreateModel model)
    {
        var existHospital = await unitOfWork.Hospitals.SelectAsync(h => h.Id == model.HospitalId && !h.IsDeleted)
            ?? throw new NotFoundException("Hospital is not found");
       
        var existDepartments = await unitOfWork.Departments.
            SelectAsync(d => d.Name.ToLower() == model.Name.ToLower() && d.HospitalId == model.HospitalId);

        if (existDepartments is not null)
            throw new AlreadyExistException("Department is already exist");

        var department = mapper.Map<Department>(model);
        department.Create();
        department.Hospital = existHospital;

        var createdDepartment = await unitOfWork.Departments.InsertAsync(department);
        await unitOfWork.SaveAsync();

        return mapper.Map<DepartmentViewModel>(createdDepartment);
    }

    public async Task<DepartmentViewModel> UpdateAsync(long id, DepartmentUpdateModel model)
    {
        var existDepartment = await unitOfWork.Departments.SelectAsync(d => d.Id == id)
            ?? throw new NotFoundException("Department is not found");

        var existHospital = await unitOfWork.Hospitals.SelectAsync(h => h.Id == model.HospitalId && !h.IsDeleted)
            ?? throw new NotFoundException("Hospital is not found");

        var alreadyExistDepartment = await unitOfWork.Departments.
           SelectAsync(d => d.Name.ToLower() == model.Name.ToLower() && d.HospitalId == model.HospitalId && d.Id != id);

        if (alreadyExistDepartment is not null)
            throw new AlreadyExistException("Department is already exist");

        mapper.Map(model, existDepartment);
        existDepartment.Update();
        existDepartment.Hospital = existHospital;

        var updatedDepartment = await unitOfWork.Departments.UpdateAsync(existDepartment);
        await unitOfWork.SaveAsync();

        return mapper.Map<DepartmentViewModel>(updatedDepartment);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existDepartment = await unitOfWork.Departments.SelectAsync(d => d.Id == id)
            ?? throw new NotFoundException("Department is not found");

        await unitOfWork.Departments.DeleteAsync(existDepartment);
        existDepartment.Delete();
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<DepartmentViewModel> GetByIdAsync(long id)
    {
        var existDepartment = await unitOfWork.Departments.
            SelectAsync(expression: d => d.Id == id && d.IsDeleted, includes: ["Hospital"])
            ?? throw new NotFoundException("Department is not found");

        return mapper.Map<DepartmentViewModel>(existDepartment);
    }

    public async Task<IEnumerable<DepartmentViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var departments = unitOfWork.Departments.
            SelectAsQueryable(expression: d => !d.IsDeleted, includes: ["Hospital"], isTracked: false).
            OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            departments = departments.Where(d =>
            d.Hospital.Name.ToLower().Contains(search.ToLower()) ||
            d.Name.ToLower().Contains(search.ToLower()));

        var paginateDepartments = await departments.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<DepartmentViewModel>>(paginateDepartments);
    }
}
