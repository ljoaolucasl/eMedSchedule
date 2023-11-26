﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eMedSchedule.Infra.Orm.Common;

#nullable disable

namespace eMedSchedule.Infra.Orm.Migrations
{
    [DbContext(typeof(EMedScheduleContext))]
    partial class EMedScheduleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FK_TBDoctorActivity_TBDoctor", b =>
                {
                    b.Property<Guid>("ActivitiesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DoctorsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ActivitiesId", "DoctorsId");

                    b.HasIndex("DoctorsId");

                    b.ToTable("FK_TBDoctorActivity_TBDoctor", (string)null);

                    b.HasData(
                        new
                        {
                            ActivitiesId = new Guid("08dbee49-5c82-d7a8-6fc2-9f4780015522"),
                            DoctorsId = new Guid("e5263897-c9e6-4234-8425-934e455697dd")
                        },
                        new
                        {
                            ActivitiesId = new Guid("08dbee49-5c82-d7d1-6fc2-9f4780015523"),
                            DoctorsId = new Guid("e5263897-c9e6-4234-8425-934e455697dd")
                        },
                        new
                        {
                            ActivitiesId = new Guid("08dbee49-5c82-d7d7-6fc2-9f4780015524"),
                            DoctorsId = new Guid("93b57152-eebb-45f0-8647-ca582580a93e")
                        },
                        new
                        {
                            ActivitiesId = new Guid("08dbee49-5c82-d7dc-6fc2-9f4780015525"),
                            DoctorsId = new Guid("edadb5a0-2b4b-4913-9d34-51fb0edb2852")
                        },
                        new
                        {
                            ActivitiesId = new Guid("08dbee49-5c82-d7e1-6fc2-9f4780015526"),
                            DoctorsId = new Guid("e5263897-c9e6-4234-8425-934e455697dd")
                        },
                        new
                        {
                            ActivitiesId = new Guid("08dbee49-5c82-d7e1-6fc2-9f4780015526"),
                            DoctorsId = new Guid("93b57152-eebb-45f0-8647-ca582580a93e")
                        },
                        new
                        {
                            ActivitiesId = new Guid("08dbee49-5c82-d7e1-6fc2-9f4780015526"),
                            DoctorsId = new Guid("edadb5a0-2b4b-4913-9d34-51fb0edb2852")
                        },
                        new
                        {
                            ActivitiesId = new Guid("08dbee49-5c82-d7e1-6fc2-9f4780015526"),
                            DoctorsId = new Guid("2093bd03-7aa1-49f9-a44c-11523684212a")
                        },
                        new
                        {
                            ActivitiesId = new Guid("08dbee49-5c82-d7e6-6fc2-9f4780015527"),
                            DoctorsId = new Guid("e5263897-c9e6-4234-8425-934e455697dd")
                        },
                        new
                        {
                            ActivitiesId = new Guid("08dbee49-5c82-d7e6-6fc2-9f4780015527"),
                            DoctorsId = new Guid("edadb5a0-2b4b-4913-9d34-51fb0edb2852")
                        },
                        new
                        {
                            ActivitiesId = new Guid("08dbee49-5c82-d7e6-6fc2-9f4780015527"),
                            DoctorsId = new Guid("2093bd03-7aa1-49f9-a44c-11523684212a")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("eMedSchedule.Domain.AuthenticationModule.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("e7944276-5214-46c7-2755-08dbede3db7d"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "6f07bdcf-9ff3-43da-9f8b-5e27808f81ab",
                            Email = "teste@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "Teste",
                            NormalizedEmail = "TESTE@GMAIL.COM",
                            NormalizedUserName = "TESTE@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEEpbfL1sGZGAQmfY11et9nzZ5tdMmLv5uVMiv4xXugJLxfksPyB7aJgai6Yym57vFQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "NQY5DMARMJNDQ7CUQJP3U4O7SYXLNANC",
                            TwoFactorEnabled = false,
                            UserName = "teste@gmail.com"
                        });
                });

            modelBuilder.Entity("eMedSchedule.Domain.DoctorActivityModule.DoctorActivity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ActivityType")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long>("EndTime")
                        .HasColumnType("bigint");

                    b.Property<long>("StartTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TBDoctorActivity", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("08dbee49-5c82-d7a8-6fc2-9f4780015522"),
                            ActivityType = 0,
                            Date = new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndTime = 468000000000L,
                            StartTime = 360000000000L,
                            Title = "Avaliação Clínica Geral",
                            UserId = new Guid("e7944276-5214-46c7-2755-08dbede3db7d")
                        },
                        new
                        {
                            Id = new Guid("08dbee49-5c82-d7d1-6fc2-9f4780015523"),
                            ActivityType = 0,
                            Date = new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndTime = 396000000000L,
                            StartTime = 360000000000L,
                            Title = "Consulta de Rotina",
                            UserId = new Guid("e7944276-5214-46c7-2755-08dbede3db7d")
                        },
                        new
                        {
                            Id = new Guid("08dbee49-5c82-d7d7-6fc2-9f4780015524"),
                            ActivityType = 0,
                            Date = new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndTime = 396000000000L,
                            StartTime = 324000000000L,
                            Title = "Exame Físico e Diagnóstico",
                            UserId = new Guid("e7944276-5214-46c7-2755-08dbede3db7d")
                        },
                        new
                        {
                            Id = new Guid("08dbee49-5c82-d7dc-6fc2-9f4780015525"),
                            ActivityType = 0,
                            Date = new DateTime(2023, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndTime = 576000000000L,
                            StartTime = 540000000000L,
                            Title = "Discussão de Resultados de Exames",
                            UserId = new Guid("e7944276-5214-46c7-2755-08dbede3db7d")
                        },
                        new
                        {
                            Id = new Guid("08dbee49-5c82-d7e1-6fc2-9f4780015526"),
                            ActivityType = 1,
                            Date = new DateTime(2023, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndTime = 720000000000L,
                            StartTime = 504000000000L,
                            Title = "Cirurgia Cardiovascular",
                            UserId = new Guid("e7944276-5214-46c7-2755-08dbede3db7d")
                        },
                        new
                        {
                            Id = new Guid("08dbee49-5c82-d7e6-6fc2-9f4780015527"),
                            ActivityType = 0,
                            Date = new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndTime = 540000000000L,
                            StartTime = 360000000000L,
                            Title = "Procedimento Ortopédico",
                            UserId = new Guid("e7944276-5214-46c7-2755-08dbede3db7d")
                        });
                });

            modelBuilder.Entity("eMedSchedule.Domain.DoctorModule.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TBDoctor", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("e5263897-c9e6-4234-8425-934e455697dd"),
                            CRM = "45872-SC",
                            Name = "Marcos",
                            UserId = new Guid("e7944276-5214-46c7-2755-08dbede3db7d")
                        },
                        new
                        {
                            Id = new Guid("93b57152-eebb-45f0-8647-ca582580a93e"),
                            CRM = "64732-SC",
                            Name = "Antônio",
                            UserId = new Guid("e7944276-5214-46c7-2755-08dbede3db7d")
                        },
                        new
                        {
                            Id = new Guid("edadb5a0-2b4b-4913-9d34-51fb0edb2852"),
                            CRM = "86463-RS",
                            Name = "Clara",
                            UserId = new Guid("e7944276-5214-46c7-2755-08dbede3db7d")
                        },
                        new
                        {
                            Id = new Guid("2093bd03-7aa1-49f9-a44c-11523684212a"),
                            CRM = "17497-SP",
                            Name = "Mateus",
                            UserId = new Guid("e7944276-5214-46c7-2755-08dbede3db7d")
                        });
                });

            modelBuilder.Entity("FK_TBDoctorActivity_TBDoctor", b =>
                {
                    b.HasOne("eMedSchedule.Domain.DoctorActivityModule.DoctorActivity", null)
                        .WithMany()
                        .HasForeignKey("ActivitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eMedSchedule.Domain.DoctorModule.Doctor", null)
                        .WithMany()
                        .HasForeignKey("DoctorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("eMedSchedule.Domain.AuthenticationModule.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("eMedSchedule.Domain.AuthenticationModule.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eMedSchedule.Domain.AuthenticationModule.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("eMedSchedule.Domain.AuthenticationModule.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eMedSchedule.Domain.DoctorActivityModule.DoctorActivity", b =>
                {
                    b.HasOne("eMedSchedule.Domain.AuthenticationModule.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("eMedSchedule.Domain.DoctorModule.Doctor", b =>
                {
                    b.HasOne("eMedSchedule.Domain.AuthenticationModule.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
