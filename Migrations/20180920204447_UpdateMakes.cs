using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vega.Migrations
{
    public partial class UpdateMakes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Makes set Name = 'Ford' where Name = 'Make1'");
            migrationBuilder.Sql("update Makes set Name = 'Renault' where Name = 'Make2'");
            migrationBuilder.Sql("update Makes set Name = 'Skoda' where Name = 'Make3'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Makes set Name = 'Make1' where Name = 'Ford'");
            migrationBuilder.Sql("update Makes set Name = 'Make2' where Name = 'Renault'");
            migrationBuilder.Sql("update Makes set Name = 'Make3' where Name = 'Skoda'");
        }
    }
}
