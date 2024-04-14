using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PetPalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracionPetpal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserSupplier = table.Column<bool>(type: "bit", nullable: false),
                    UserRating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductAvailability = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductOnline = table.Column<bool>(type: "bit", nullable: false),
                    ProductStock = table.Column<int>(type: "int", nullable: false),
                    ProductRating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceAvailability = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceOnline = table.Column<bool>(type: "bit", nullable: false),
                    ServiceRating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                    table.ForeignKey(
                        name: "FK_Services_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserEmail", "UserName", "UserPassword", "UserRating", "UserRegisterDate", "UserRole", "UserSupplier" },
                values: new object[,]
                {
                    { 1, "ruben@gmail.com.com", "Ruben", "patatas1", 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", true },
                    { 2, "xio@gmail.com", "Xio", "patatas2", 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Client", false },
                    { 3, "carlota@gmail.com", "Carlota", "patatas3", 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Client", false },
                    { 4, "alberto@gmail.com", "Alberto", "patatas4", 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Client", true },
                    { 5, "alejandro@gmail.com", "Alejandro", "patatas5", 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Client", true }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ProductAvailability", "ProductDescription", "ProductName", "ProductOnline", "ProductPrice", "ProductRating", "ProductStock", "ProductType", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dog food for dogs", "Dog food", true, 10.0m, 4.5, 10, "Food", 1 },
                    { 2, new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Interactive cat toy", "Cat toy", true, 15.0m, 4.7999999999999998, 20, "Toy", 2 },
                    { 3, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Durable dog leash", "Leash", true, 20.0m, 4.7000000000000002, 30, "Accessory", 3 },
                    { 4, new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nutritional food for parrots", "Parrot food", true, 25.0m, 4.9000000000000004, 40, "Food", 4 },
                    { 5, new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Organic pet shampoo", "Shampoo", true, 30.0m, 4.5999999999999996, 50, "Grooming", 5 }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "ServiceAvailability", "ServiceDescription", "ServiceName", "ServiceOnline", "ServicePrice", "ServiceRating", "ServiceType", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Basic pet grooming service", "Basic Grooming", true, 50.0m, 4.5, "Grooming", 1 },
                    { 2, new DateTime(2022, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Basic obedience training for dogs", "Obedience Training", true, 200.0m, 4.7999999999999998, "Training", 2 },
                    { 3, new DateTime(2022, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pet sitting for all kinds of pets", "Pet Sitting", true, 30.0m, 4.7000000000000002, "Sitting", 3 },
                    { 4, new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daily dog walking service", "Dog Walking", true, 15.0m, 4.9000000000000004, "Walking", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId",
                table: "Services",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
