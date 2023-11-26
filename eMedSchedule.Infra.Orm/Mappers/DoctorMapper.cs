using eMedSchedule.Domain.DoctorModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedSchedule.Infra.Orm.Mappers
{
    public class DoctorMapper : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("TBDoctor");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(d => d.CRM).HasColumnType("varchar(100)").IsRequired();
            builder.Property(d => d.ProfilePicture);

            builder.HasMany(x => x.Activities)
                .WithMany(x => x.Doctors)
                .UsingEntity(x => x.ToTable("FK_TBDoctorActivity_TBDoctor"));

            builder.HasOne(x => x.User)
                .WithMany()
                .IsRequired()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            Guid userId = Guid.Parse("E7944276-5214-46C7-2755-08DBEDE3DB7D");

            var doctor1 = new Doctor("Marcos", "45872-SC", null) { UserId = userId, Id = Guid.Parse("e5263897-c9e6-4234-8425-934e455697dd") };

            var doctor2 = new Doctor("Antônio", "64732-SC", null) { UserId = userId, Id = Guid.Parse("93b57152-eebb-45f0-8647-ca582580a93e") };

            var doctor3 = new Doctor("Clara", "86463-RS", null) { UserId = userId, Id = Guid.Parse("edadb5a0-2b4b-4913-9d34-51fb0edb2852") };

            var doctor4 = new Doctor("Mateus", "17497-SP", null) { UserId = userId, Id = Guid.Parse("2093bd03-7aa1-49f9-a44c-11523684212a") };

            builder.HasData(doctor1, doctor2, doctor3, doctor4);
        }
    }
}