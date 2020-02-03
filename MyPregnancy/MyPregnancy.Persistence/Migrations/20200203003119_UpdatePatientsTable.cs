using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPregnancy.Persistence.Migrations
{
    public partial class UpdatePatientsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HospitalEnrolment_Hospital_HospitalId",
                table: "HospitalEnrolment");

            migrationBuilder.DropIndex(
                name: "IX_HospitalEnrolment_HospitalId",
                table: "HospitalEnrolment");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Patient");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Patient",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Patient");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "Patient",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Patient",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Patient",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HospitalEnrolment_HospitalId",
                table: "HospitalEnrolment",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalEnrolment_Hospital_HospitalId",
                table: "HospitalEnrolment",
                column: "HospitalId",
                principalTable: "Hospital",
                principalColumn: "HospitalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
