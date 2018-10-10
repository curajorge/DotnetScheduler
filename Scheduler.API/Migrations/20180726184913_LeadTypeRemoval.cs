using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scheduler.API.Migrations
{
    public partial class LeadTypeRemoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdated",
                table: "Schedule",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 26, 14, 49, 13, 650, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Schedule",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 26, 14, 49, 13, 650, DateTimeKind.Local));

            migrationBuilder.AlterColumn<int>(
                name: "Lead",
                table: "Customer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Customer",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 26, 14, 49, 13, 631, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdated",
                table: "Schedule",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 26, 13, 23, 37, 795, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Schedule",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 26, 13, 23, 37, 795, DateTimeKind.Local));

            migrationBuilder.AlterColumn<int>(
                name: "Lead",
                table: "Customer",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Customer",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 26, 13, 23, 37, 777, DateTimeKind.Local));
        }
    }
}
