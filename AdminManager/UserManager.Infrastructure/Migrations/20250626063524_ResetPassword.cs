using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ResetPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PasswordHash",
                value: "$2a$11$X46NFh.5VRZzPMljoZwPN.RtbxoopbjysG.LOIFkslMBA4Xu2J50m");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PasswordHash",
                value: "$2a$11$ScBPsrMo1F5G5V.6T/DxtuaeRlerp.2HEe69nSWYWOTDYfShKSDiS");
        }
    }
}
