using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetPalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmailInSerPro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "UserEmail",
                value: "ruben@gmail.com.com");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "UserEmail",
                value: "xio@gmail.com");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "UserEmail",
                value: "carlota@gmail.com");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "UserEmail",
                value: "alberto@gmail.com");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "UserEmail",
                value: "alejandro@gmail.com");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 1,
                column: "UserEmail",
                value: "ruben@gmail.com.com");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 2,
                column: "UserEmail",
                value: "xio@gmail.com");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 3,
                column: "UserEmail",
                value: "carlota@gmail.com");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 4,
                column: "UserEmail",
                value: "alejandro@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "UserPassword",
                value: "$2a$11$xycRJn0CdDY5peQPcNX/duZ3zQydv3HW5s16KPll8zf15g3DWvBJ2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "UserPassword",
                value: "$2a$11$Imc4jJ2ya.4iL9ejpxB4mOaK2drBl8u97OC/zNLHgHFWb1KtffDQK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "UserPassword",
                value: "$2a$11$2dxdRZ1XvfeJxs6CA087Suj2zI17bPm1OTEGP4gCjTHWFV/5IRYje");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "UserPassword",
                value: "$2a$11$FsT7bup.zMuCf7PcYQzC5eTN4.O9e54kQlX8JTVIhFYclEYzgNWSi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "UserPassword",
                value: "$2a$11$d0SDw/zmYckhnlssxew76.OQWW1S25JFsIp5dLsYEgMEQIJo7inHW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Products");

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
    }
}
