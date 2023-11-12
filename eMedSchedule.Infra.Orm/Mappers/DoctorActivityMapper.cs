﻿using eMedSchedule.Domain.DoctorActivityModule;
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
            builder.Property(d => d.StartTime).IsRequired();
            builder.Property(d => d.EndTime).IsRequired();
            builder.Property(d => d.RecoveryTime).HasColumnType("bigint").IsRequired();

            builder.HasMany(d => d.Doctors)
                .WithMany()
                .UsingEntity(x => x.ToTable("FK_TBDoctorActivity_TBDoctor"));
        }
    }
}