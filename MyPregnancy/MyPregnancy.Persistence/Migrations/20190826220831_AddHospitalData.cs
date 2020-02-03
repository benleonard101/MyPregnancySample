using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPregnancy.Persistence.Migrations
{
    public partial class AddHospitalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[Hospital] ON;");
            migrationBuilder.Sql("INSERT[dbo].[Hospital] ( [HospitalId], [Name]) VALUES('0' , 'Altnagelvin')");
            migrationBuilder.Sql("INSERT[dbo].[Hospital] ( [HospitalId], [Name]) VALUES('1' , 'Antrim Area')");
            migrationBuilder.Sql("INSERT[dbo].[Hospital] ( [HospitalId], [Name]) VALUES('2' , 'Causeway')");
            migrationBuilder.Sql("INSERT[dbo].[Hospital] ( [HospitalId], [Name]) VALUES('3' , 'Craigavon')");
            migrationBuilder.Sql("INSERT[dbo].[Hospital] ( [HospitalId], [Name]) VALUES('4' , 'Daisy Hill')");
            migrationBuilder.Sql("INSERT[dbo].[Hospital] ( [HospitalId], [Name]) VALUES('5' , 'Downpatrick')");
            migrationBuilder.Sql("INSERT[dbo].[Hospital] ( [HospitalId], [Name]) VALUES('6' , 'Lagan Valley')");
            migrationBuilder.Sql("INSERT[dbo].[Hospital] ( [HospitalId], [Name]) VALUES('7' , 'Mater')");
            migrationBuilder.Sql("INSERT[dbo].[Hospital] ( [HospitalId], [Name]) VALUES('8' , 'Royal Jubilee')");
            migrationBuilder.Sql("INSERT[dbo].[Hospital] ( [HospitalId], [Name]) VALUES('9' , 'South West Acute')");
            migrationBuilder.Sql("INSERT[dbo].[Hospital] ( [HospitalId], [Name]) VALUES('10' , 'Ulster')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[Hospital] OFF;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
