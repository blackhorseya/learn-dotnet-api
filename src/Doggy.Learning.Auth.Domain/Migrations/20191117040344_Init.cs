using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doggy.Learning.Auth.Domain.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Groups",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAt = table.Column<DateTime>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Groups", x => x.Id); });

            migrationBuilder.CreateTable(
                "Modules",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAt = table.Column<DateTime>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Modules", x => x.Id); });

            migrationBuilder.CreateTable(
                "Roles",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAt = table.Column<DateTime>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Roles", x => x.Id); });

            migrationBuilder.CreateTable(
                "Services",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAt = table.Column<DateTime>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Services", x => x.Id); });

            migrationBuilder.CreateTable(
                "GroupRoleMap",
                table => new
                {
                    GroupId = table.Column<int>(),
                    RoleId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRoleMap", x => new {x.RoleId, x.GroupId});
                    table.ForeignKey(
                        "FK_GroupRoleMap_Groups_GroupId",
                        x => x.GroupId,
                        "Groups",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_GroupRoleMap_Roles_RoleId",
                        x => x.RoleId,
                        "Roles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "RoleModuleMap",
                table => new
                {
                    RoleId = table.Column<int>(),
                    ModuleId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleModuleMap", x => new {x.RoleId, x.ModuleId});
                    table.ForeignKey(
                        "FK_RoleModuleMap_Modules_ModuleId",
                        x => x.ModuleId,
                        "Modules",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_RoleModuleMap_Roles_RoleId",
                        x => x.RoleId,
                        "Roles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "RoleServiceMap",
                table => new
                {
                    RoleId = table.Column<int>(),
                    ServiceId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleServiceMap", x => new {x.RoleId, x.ServiceId});
                    table.ForeignKey(
                        "FK_RoleServiceMap_Roles_RoleId",
                        x => x.RoleId,
                        "Roles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_RoleServiceMap_Services_ServiceId",
                        x => x.ServiceId,
                        "Services",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_GroupRoleMap_GroupId",
                "GroupRoleMap",
                "GroupId");

            migrationBuilder.CreateIndex(
                "IX_RoleModuleMap_ModuleId",
                "RoleModuleMap",
                "ModuleId");

            migrationBuilder.CreateIndex(
                "IX_RoleServiceMap_ServiceId",
                "RoleServiceMap",
                "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "GroupRoleMap");

            migrationBuilder.DropTable(
                "RoleModuleMap");

            migrationBuilder.DropTable(
                "RoleServiceMap");

            migrationBuilder.DropTable(
                "Groups");

            migrationBuilder.DropTable(
                "Modules");

            migrationBuilder.DropTable(
                "Roles");

            migrationBuilder.DropTable(
                "Services");
        }
    }
}