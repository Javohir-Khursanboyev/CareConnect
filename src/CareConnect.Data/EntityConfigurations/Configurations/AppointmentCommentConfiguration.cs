using CareConnect.Domain.Entities.Appointments;
using CareConnect.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Arcana.DataAccess.EntityConfigurations.Commons;

public class AppointmentCommentConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder modelBuilder)
    {
        // Appointment and Patient
        modelBuilder.Entity<Appointment>()
            .HasOne(appointment => appointment.Patient)
            .WithMany(patient => patient.Appointments)
            .HasForeignKey(appointment => appointment.PatientId)
            .OnDelete(DeleteBehavior.NoAction);

        // Appointment and Doctor
        modelBuilder.Entity<Appointment>()
            .HasOne(appointment => appointment.Doctor)
            .WithMany(doctor => doctor.Appointments)
            .HasForeignKey(appointment => appointment.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>().HasData(
                 new Appointment
                 {
                     Id = 1,
                     DoctorId = 1,
                     PatientId = 1,
                     Date = DateTime.Now,
                     Duration = 60,
                     Status = AppointmentStatus.Scheduled,
                     CreatedAt = DateTime.UtcNow,
                 });
    }
}