using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
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
                    Id = table.Column<string>(type: "text", nullable: false),
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
                name: "color",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    hex_code = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_color", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fuel_type",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fuel_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "maintenance_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_maintenance_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "manufacturer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    founded_year = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_category",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
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
                    UserId = table.Column<string>(type: "text", nullable: false),
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
                    UserId = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false),
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
                name: "user",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    role_id = table.Column<short>(type: "smallint", nullable: true),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    middle_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    passport_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    phone = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vehicle_model",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    manufacturer_id = table.Column<long>(type: "bigint", nullable: false),
                    vehicle_category_id = table.Column<short>(type: "smallint", nullable: false),
                    power = table.Column<short>(type: "smallint", nullable: false),
                    fuel_type_id = table.Column<short>(type: "smallint", nullable: false),
                    load_capacity = table.Column<int>(type: "integer", nullable: false),
                    Co2Emissions = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_model", x => x.id);
                    table.ForeignKey(
                        name: "FK_vehicle_model_fuel_type_fuel_type_id",
                        column: x => x.fuel_type_id,
                        principalTable: "fuel_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vehicle_model_manufacturer_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalTable: "manufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vehicle_model_vehicle_category_vehicle_category_id",
                        column: x => x.vehicle_category_id,
                        principalTable: "vehicle_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicle",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    model_id = table.Column<int>(type: "integer", nullable: false),
                    color_id = table.Column<int>(type: "integer", nullable: true),
                    vin = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    production_year = table.Column<short>(type: "smallint", nullable: false),
                    mileage = table.Column<int>(type: "integer", nullable: false),
                    registration_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vehicle_color_color_id",
                        column: x => x.color_id,
                        principalTable: "color",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_vehicle_vehicle_model_model_id",
                        column: x => x.model_id,
                        principalTable: "vehicle_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "insurance",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vehicle_id = table.Column<long>(type: "bigint", nullable: false),
                    policy_number = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    company = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_insurance_vehicle_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "maintenance",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vehicle_id = table.Column<long>(type: "bigint", nullable: false),
                    maintenance_type_id = table.Column<int>(type: "integer", nullable: false),
                    service_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: true),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_maintenance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_maintenance_maintenance_type_maintenance_type_id",
                        column: x => x.maintenance_type_id,
                        principalTable: "maintenance_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_maintenance_vehicle_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ownership_history",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vehicle_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ownership_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ownership_history_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ownership_history_vehicle_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "name" },
                values: new object[] { (short)1, "admin" });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "first_name", "last_name", "login", "middle_name", "passport_number", "password_hash", "phone", "role_id" },
                values: new object[] { 1L, null, null, "admin", null, null, "21232f297a57a5a743894a0e4a801fc3", null, (short)1 });

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
                name: "IX_color_name",
                table: "color",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fuel_type_name",
                table: "fuel_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_insurance_vehicle_id",
                table: "insurance",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_maintenance_type_id",
                table: "maintenance",
                column: "maintenance_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_vehicle_id",
                table: "maintenance",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_type_name",
                table: "maintenance_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ownership_history_user_id",
                table: "ownership_history",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ownership_history_vehicle_id",
                table: "ownership_history",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_name",
                table: "role",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_login",
                table: "user",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_passport_number",
                table: "user",
                column: "passport_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_color_id",
                table: "vehicle",
                column: "color_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_model_id",
                table: "vehicle",
                column: "model_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_registration_number",
                table: "vehicle",
                column: "registration_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_vin",
                table: "vehicle",
                column: "vin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_category_name",
                table: "vehicle_category",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_model_fuel_type_id",
                table: "vehicle_model",
                column: "fuel_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_model_manufacturer_id",
                table: "vehicle_model",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_model_vehicle_category_id",
                table: "vehicle_model",
                column: "vehicle_category_id");
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
                name: "insurance");

            migrationBuilder.DropTable(
                name: "maintenance");

            migrationBuilder.DropTable(
                name: "ownership_history");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "maintenance_type");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "vehicle");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "color");

            migrationBuilder.DropTable(
                name: "vehicle_model");

            migrationBuilder.DropTable(
                name: "fuel_type");

            migrationBuilder.DropTable(
                name: "manufacturer");

            migrationBuilder.DropTable(
                name: "vehicle_category");
        }
    }
}
