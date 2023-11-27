using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eMedSchedule.Infra.Orm.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBDoctor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    CRM = table.Column<string>(type: "varchar(100)", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "bytea", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBDoctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBDoctor_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TBDoctorActivity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false),
                    ActivityType = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBDoctorActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBDoctorActivity_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FK_TBDoctorActivity_TBDoctor",
                columns: table => new
                {
                    ActivitiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FK_TBDoctorActivity_TBDoctor", x => new { x.ActivitiesId, x.DoctorsId });
                    table.ForeignKey(
                        name: "FK_FK_TBDoctorActivity_TBDoctor_TBDoctorActivity_ActivitiesId",
                        column: x => x.ActivitiesId,
                        principalTable: "TBDoctorActivity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FK_TBDoctorActivity_TBDoctor_TBDoctor_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "TBDoctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("e7944276-5214-46c7-2755-08dbede3db7d"), 0, "6f07bdcf-9ff3-43da-9f8b-5e27808f81ab", "teste@gmail.com", true, false, null, "Teste", "TESTE@GMAIL.COM", "TESTE@GMAIL.COM", "AQAAAAIAAYagAAAAEEpbfL1sGZGAQmfY11et9nzZ5tdMmLv5uVMiv4xXugJLxfksPyB7aJgai6Yym57vFQ==", null, false, "NQY5DMARMJNDQ7CUQJP3U4O7SYXLNANC", false, "teste@gmail.com" });

            migrationBuilder.InsertData(
                table: "TBDoctor",
                columns: new[] { "Id", "CRM", "Name", "ProfilePicture", "UserId" },
                values: new object[,]
                {
                    { new Guid("2093bd03-7aa1-49f9-a44c-11523684212a"), "17497-SP", "Mateus", null, new Guid("e7944276-5214-46c7-2755-08dbede3db7d") },
                    { new Guid("93b57152-eebb-45f0-8647-ca582580a93e"), "64732-SC", "Antônio", null, new Guid("e7944276-5214-46c7-2755-08dbede3db7d") },
                    { new Guid("e5263897-c9e6-4234-8425-934e455697dd"), "45872-SC", "Marcos", null, new Guid("e7944276-5214-46c7-2755-08dbede3db7d") },
                    { new Guid("edadb5a0-2b4b-4913-9d34-51fb0edb2852"), "86463-RS", "Clara", null, new Guid("e7944276-5214-46c7-2755-08dbede3db7d") }
                });

            migrationBuilder.InsertData(
                table: "TBDoctorActivity",
                columns: new[] { "Id", "ActivityType", "Date", "EndTime", "StartTime", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("08dbef06-62fb-12ce-6fc2-9f14e00503c6"), 0, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 13, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), "Avaliação Clínica Geral", new Guid("e7944276-5214-46c7-2755-08dbede3db7d") },
                    { new Guid("08dbef06-62fb-12e8-6fc2-9f14e00503c7"), 0, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), "Consulta de Rotina", new Guid("e7944276-5214-46c7-2755-08dbede3db7d") },
                    { new Guid("08dbef06-62fb-12ed-6fc2-9f14e00503c8"), 0, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), "Exame Físico e Diagnóstico", new Guid("e7944276-5214-46c7-2755-08dbede3db7d") },
                    { new Guid("08dbef06-62fb-12f1-6fc2-9f14e00503c9"), 0, new DateTime(2023, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 15, 0, 0, 0), "Discussão de Resultados de Exames", new Guid("e7944276-5214-46c7-2755-08dbede3db7d") },
                    { new Guid("08dbef06-62fb-12f4-6fc2-9f14e00503ca"), 1, new DateTime(2023, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 14, 0, 0, 0), "Cirurgia Cardiovascular", new Guid("e7944276-5214-46c7-2755-08dbede3db7d") },
                    { new Guid("08dbef06-62fb-12f8-6fc2-9f14e00503cb"), 0, new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 15, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), "Procedimento Ortopédico", new Guid("e7944276-5214-46c7-2755-08dbede3db7d") }
                });

            migrationBuilder.InsertData(
                table: "FK_TBDoctorActivity_TBDoctor",
                columns: new[] { "ActivitiesId", "DoctorsId" },
                values: new object[,]
                {
                    { new Guid("08dbef06-62fb-12ce-6fc2-9f14e00503c6"), new Guid("e5263897-c9e6-4234-8425-934e455697dd") },
                    { new Guid("08dbef06-62fb-12e8-6fc2-9f14e00503c7"), new Guid("e5263897-c9e6-4234-8425-934e455697dd") },
                    { new Guid("08dbef06-62fb-12ed-6fc2-9f14e00503c8"), new Guid("93b57152-eebb-45f0-8647-ca582580a93e") },
                    { new Guid("08dbef06-62fb-12f1-6fc2-9f14e00503c9"), new Guid("edadb5a0-2b4b-4913-9d34-51fb0edb2852") },
                    { new Guid("08dbef06-62fb-12f4-6fc2-9f14e00503ca"), new Guid("2093bd03-7aa1-49f9-a44c-11523684212a") },
                    { new Guid("08dbef06-62fb-12f4-6fc2-9f14e00503ca"), new Guid("93b57152-eebb-45f0-8647-ca582580a93e") },
                    { new Guid("08dbef06-62fb-12f4-6fc2-9f14e00503ca"), new Guid("e5263897-c9e6-4234-8425-934e455697dd") },
                    { new Guid("08dbef06-62fb-12f4-6fc2-9f14e00503ca"), new Guid("edadb5a0-2b4b-4913-9d34-51fb0edb2852") },
                    { new Guid("08dbef06-62fb-12f8-6fc2-9f14e00503cb"), new Guid("2093bd03-7aa1-49f9-a44c-11523684212a") },
                    { new Guid("08dbef06-62fb-12f8-6fc2-9f14e00503cb"), new Guid("e5263897-c9e6-4234-8425-934e455697dd") },
                    { new Guid("08dbef06-62fb-12f8-6fc2-9f14e00503cb"), new Guid("edadb5a0-2b4b-4913-9d34-51fb0edb2852") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FK_TBDoctorActivity_TBDoctor_DoctorsId",
                table: "FK_TBDoctorActivity_TBDoctor",
                column: "DoctorsId");

            migrationBuilder.CreateIndex(
                name: "IX_TBDoctor_UserId",
                table: "TBDoctor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TBDoctorActivity_UserId",
                table: "TBDoctorActivity",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FK_TBDoctorActivity_TBDoctor");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TBDoctorActivity");

            migrationBuilder.DropTable(
                name: "TBDoctor");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
