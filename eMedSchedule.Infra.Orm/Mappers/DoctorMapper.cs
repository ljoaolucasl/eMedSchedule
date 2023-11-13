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

            builder.HasMany(x => x.Activities);
        }
    }
}