using AutoMapper;
using CareConnect.Service.DTOs.Users;
using CareConnect.Service.DTOs.Roles;
using CareConnect.Service.DTOs.Assets;
using CareConnect.Service.DTOs.Doctors;
using CareConnect.Service.DTOs.Patients;
using CareConnect.Domain.Entities.Users;
using CareConnect.Domain.Entities.Assets;
using CareConnect.Service.DTOs.Hospitals;
using CareConnect.Domain.Entities.Doctors;
using CareConnect.Domain.Entities.Patients;
using CareConnect.Service.DTOs.Departments;
using CareConnect.Service.DTOs.DoctorStars;
using CareConnect.Service.DTOs.Permissions;
using CareConnect.Domain.Entities.Documents;
using CareConnect.Domain.Entities.Hospitals;
using CareConnect.Service.DTOs.Appointments;
using CareConnect.Domain.Entities.Departments;
using CareConnect.Service.DTOs.DoctorComments;
using CareConnect.Domain.Entities.DoctorStars;
using CareConnect.Domain.Entities.Appointments;
using CareConnect.Service.DTOs.RolePermissions;
using CareConnect.Domain.Entities.DoctorComments;
using CareConnect.Domain.Entities.Recommendations;

namespace CareConnect.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppointmentCreateModel, Appointment>().ReverseMap();
        CreateMap<AppointmentUpdateModel, Appointment>().ReverseMap();
        CreateMap<Appointment, AppointmentViewModel>().ReverseMap();

        CreateMap<AssetCreateModel, Asset>().ReverseMap();
        CreateMap<Asset, AssetViewModel>().ReverseMap();

        CreateMap<DepartmentCreateModel, Department>().ReverseMap();
        CreateMap<DepartmentUpdateModel, Department>().ReverseMap();
        CreateMap<Department, DepartmentViewModel>().ReverseMap();

        CreateMap<DoctorCommentCreateModel, DoctorComment>().ReverseMap();
        CreateMap<DoctorCommentUpdateModel, DoctorComment>().ReverseMap();
        CreateMap<DoctorComment, DoctorCommentViewModel>().ReverseMap();

        CreateMap<DoctorCreateModel, Doctor>().ReverseMap();
        CreateMap<DoctorUpdateModel, Doctor>().ReverseMap();
        CreateMap<Doctor, DoctorViewModel>().ReverseMap();

        CreateMap<DoctorStarCreateModel, DoctorStar>().ReverseMap();
        CreateMap<DoctorStar, DoctorStarViewModel>().ReverseMap();

        CreateMap<Document, DocumentsCreateModel>().ReverseMap();
        CreateMap<Document, DocumentViewModel>().ReverseMap();

        CreateMap<HospitalCreateModel, Hospital>().ReverseMap();
        CreateMap<HospitalUpdateModel, Hospital>().ReverseMap();
        CreateMap<Hospital, HospitalViewModel>().ReverseMap();

        CreateMap<PatientCreateModel, Patient>().ReverseMap();
        CreateMap<PatientUpdateModel, Patient>().ReverseMap();
        CreateMap<Patient, PatientViewModel>().ReverseMap();

        CreateMap<PermissionCreateModel, Permission>().ReverseMap();
        CreateMap<PermissionUpdateModel, Permission>().ReverseMap();
        CreateMap<Permission, PermissionViewModel>().ReverseMap();

        CreateMap<RecommendationCreateModel, Recommendation>().ReverseMap();
        CreateMap<Recommendation, RecommendationViewModel>().ReverseMap();

        CreateMap<RolePermissionCreateModel, RolePermission>().ReverseMap();
        CreateMap<RolePermissionUpdateModel, RolePermission>().ReverseMap();
        CreateMap<RolePermission, RolePermissionViewModel>().ReverseMap();

        CreateMap<RoleCreateModel, Role>().ReverseMap();
        CreateMap<RoleUpdateModel, Role>().ReverseMap();
        CreateMap<Role, RoleViewModel>().ReverseMap();

        CreateMap<UserCreateModel, User>().ReverseMap();
        CreateMap<UserUpdateModel, User>().ReverseMap();
        CreateMap<User, UserViewModel>().ReverseMap();
    }
}
