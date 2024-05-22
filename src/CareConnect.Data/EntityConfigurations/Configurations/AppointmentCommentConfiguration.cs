using CareConnect.Domain.Entities.Appointments;
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

    }
}