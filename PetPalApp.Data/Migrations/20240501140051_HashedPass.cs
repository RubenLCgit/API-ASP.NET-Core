using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetPalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class HashedPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "UserPassword",
                value: "$2a$11$EBdVPnIKmER7a8iER0HER.TMJvzEgz1a6sR7.C0To5y8CdpVNsXOm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "UserPassword",
                value: "$2a$11$0mILplP1OlsR6ytNJOCUxeHQ69wTmuZH6ui/bjq5O5RfXMDLjZyWG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "UserPassword",
                value: "$2a$11$aB2J/hOZFLPyax8z0UeBU.0skT1JsSAxLkNi4pDW1eeYT.nZntLxS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "UserPassword",
                value: "$2a$11$n07m5xvEV6H72u08bXZlzenm0X8KcuRy4WNswHTqHA.gOTiNKlUMO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "UserPassword",
                value: "$2a$11$AZtyECLMZcZcmxMCt.7boOjsZQL8xBkJtSbyKRQknuBGRsDAJ7/Ca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "UserPassword",
                value: "patatas1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "UserPassword",
                value: "patatas2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "UserPassword",
                value: "patatas3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "UserPassword",
                value: "patatas4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "UserPassword",
                value: "patatas5");
        }
    }
}
