using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMedSchedule.Infra.Orm.Migrations
{
    /// <inheritdoc />
    public partial class AddeMedScheduleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBDoctor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    CRM = table.Column<string>(type: "varchar(100)", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBDoctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBDoctorActivity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<long>(type: "bigint", nullable: false),
                    EndTime = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBDoctorActivity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FK_TBDoctorActivity_TBDoctor",
                columns: table => new
                {
                    ActivitiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_FK_TBDoctorActivity_TBDoctor_DoctorsId",
                table: "FK_TBDoctorActivity_TBDoctor",
                column: "DoctorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FK_TBDoctorActivity_TBDoctor");

            migrationBuilder.DropTable(
                name: "TBDoctorActivity");

            migrationBuilder.DropTable(
                name: "TBDoctor");
        }
    }
}