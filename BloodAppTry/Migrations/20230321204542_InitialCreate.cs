using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodAppTry.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "BloodBank",
                columns: table => new
                {
                    BloodBankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodBank", x => x.BloodBankID);
                });

            migrationBuilder.CreateTable(
                name: "Donor",
                columns: table => new
                {
                    DonorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donor", x => x.DonorID);
                });

            migrationBuilder.CreateTable(
                name: "DonationCenter",
                columns: table => new
                {
                    DonationCenterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OpensAt = table.Column<int>(type: "int", nullable: false),
                    ClosesAt = table.Column<int>(type: "int", nullable: false),
                    BloodBankID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationCenter", x => x.DonationCenterID);
                    table.ForeignKey(
                        name: "FK_DonationCenter_BloodBank_BloodBankID",
                        column: x => x.BloodBankID,
                        principalTable: "BloodBank",
                        principalColumn: "BloodBankID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonorID = table.Column<int>(type: "int", nullable: false),
                    DonationCenterID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_DonationCenter_DonationCenterID",
                        column: x => x.DonationCenterID,
                        principalTable: "DonationCenter",
                        principalColumn: "DonationCenterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointment_Donor_DonorID",
                        column: x => x.DonorID,
                        principalTable: "Donor",
                        principalColumn: "DonorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    DoctorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonationCenterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.DoctorID);
                    table.ForeignKey(
                        name: "FK_Doctor_DonationCenter_DonationCenterID",
                        column: x => x.DonationCenterID,
                        principalTable: "DonationCenter",
                        principalColumn: "DonationCenterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_DonationCenterID",
                table: "Appointment",
                column: "DonationCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_DonorID",
                table: "Appointment",
                column: "DonorID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DonationCenterID",
                table: "Doctor",
                column: "DonationCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_DonationCenter_BloodBankID",
                table: "DonationCenter",
                column: "BloodBankID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Donor");

            migrationBuilder.DropTable(
                name: "DonationCenter");

            migrationBuilder.DropTable(
                name: "BloodBank");
        }
    }
}
