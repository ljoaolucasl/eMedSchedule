using eMedSchedule.Domain.DoctorActivityModule;
using eMedSchedule.Domain.DoctorModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedSchedule.Infra.Orm.Mappers
{
    public class DoctorActivityMapper : IEntityTypeConfiguration<DoctorActivity>
    {
        public void Configure(EntityTypeBuilder<DoctorActivity> builder)
        {
            builder.ToTable("TBDoctorActivity");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Title).HasColumnType("varchar(100)").IsRequired();
            builder.Property(d => d.ActivityType).IsRequired();
            builder.Property(d => d.Date).IsRequired();
            builder.Property(d => d.StartTime).HasColumnType("bigint").IsRequired();
            builder.Property(d => d.EndTime).HasColumnType("bigint").IsRequired();

            builder.HasMany(d => d.Doctors)
                .WithMany(c => c.Activities);

            builder.HasOne(x => x.User)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            Guid userId = Guid.Parse("E7944276-5214-46C7-2755-08DBEDE3DB7D");

            var activity1 = new DoctorActivity("Avaliação Clínica Geral", ActivityTypeEnum.Appointment,
                            new DateTime(2023, 10, 10), new TimeSpan(10, 0, 0), new TimeSpan(13, 0, 0))
            {
                UserId = userId
            };

            var activity2 = new DoctorActivity("Consulta de Rotina", ActivityTypeEnum.Appointment,
                    new DateTime(2023, 10, 10), new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0))
            {
                UserId = userId
            };

            var activity3 = new DoctorActivity("Exame Físico e Diagnóstico", ActivityTypeEnum.Appointment,
                    new DateTime(2023, 10, 10), new TimeSpan(9, 0, 0), new TimeSpan(11, 0, 0))
            {
                UserId = userId
            };

            var activity4 = new DoctorActivity("Discussão de Resultados de Exames", ActivityTypeEnum.Appointment,
                    new DateTime(2023, 9, 11), new TimeSpan(15, 0, 0), new TimeSpan(16, 0, 0))
            {
                UserId = userId
            };

            var activity5 = new DoctorActivity("Cirurgia Cardiovascular", ActivityTypeEnum.Surgery,
                    new DateTime(2023, 11, 2), new TimeSpan(14, 0, 0), new TimeSpan(20, 0, 0))
            {
                UserId = userId
            };
            var activity6 = new DoctorActivity("Procedimento Ortopédico", ActivityTypeEnum.Appointment,
                    new DateTime(2023, 5, 5), new TimeSpan(10, 0, 0), new TimeSpan(15, 0, 0))
            {
                UserId = userId
            };

            builder.HasData(activity1, activity2, activity3, activity4, activity5, activity6);

            builder.HasMany(x => x.Doctors)
                .WithMany(x => x.Activities)
                .UsingEntity<Dictionary<string, object>>(
                    "FK_TBDoctorActivity_TBDoctor",
                    d => d.HasOne<Doctor>().WithMany().HasForeignKey("DoctorsId"),
                    a => a.HasOne<DoctorActivity>().WithMany().HasForeignKey("ActivitiesId"),
                    ad =>
                    {
                        ad.HasKey("ActivitiesId", "DoctorsId");
                        ad.HasData(
                            new
                            {
                                ActivitiesId = activity1.Id,
                                DoctorsId = Guid.Parse("e5263897-c9e6-4234-8425-934e455697dd")
                            },
                            new
                            {
                                ActivitiesId = activity2.Id,
                                DoctorsId = Guid.Parse("e5263897-c9e6-4234-8425-934e455697dd")
                            },
                            new
                            {
                                ActivitiesId = activity3.Id,
                                DoctorsId = Guid.Parse("93b57152-eebb-45f0-8647-ca582580a93e")
                            },
                            new
                            {
                                ActivitiesId = activity4.Id,
                                DoctorsId = Guid.Parse("edadb5a0-2b4b-4913-9d34-51fb0edb2852")
                            },
                            new
                            {
                                ActivitiesId = activity5.Id,
                                DoctorsId = Guid.Parse("e5263897-c9e6-4234-8425-934e455697dd")
                            },
                            new
                            {
                                ActivitiesId = activity5.Id,
                                DoctorsId = Guid.Parse("93b57152-eebb-45f0-8647-ca582580a93e")
                            },
                            new
                            {
                                ActivitiesId = activity5.Id,
                                DoctorsId = Guid.Parse("edadb5a0-2b4b-4913-9d34-51fb0edb2852")
                            },
                            new
                            {
                                ActivitiesId = activity5.Id,
                                DoctorsId = Guid.Parse("2093bd03-7aa1-49f9-a44c-11523684212a")
                            },
                            new
                            {
                                ActivitiesId = activity6.Id,
                                DoctorsId = Guid.Parse("e5263897-c9e6-4234-8425-934e455697dd")
                            },
                            new
                            {
                                ActivitiesId = activity6.Id,
                                DoctorsId = Guid.Parse("edadb5a0-2b4b-4913-9d34-51fb0edb2852")
                            },
                            new
                            {
                                ActivitiesId = activity6.Id,
                                DoctorsId = Guid.Parse("2093bd03-7aa1-49f9-a44c-11523684212a")
                            });
                    });
        }
    }
}