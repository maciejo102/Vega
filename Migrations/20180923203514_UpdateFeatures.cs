using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vega.Migrations
{
    public partial class UpdateFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Features set Name = 'ABS' where Name = 'Feature1'");
            migrationBuilder.Sql("update Features set Name = 'Air condition' where Name = 'Feature1'");
            migrationBuilder.Sql("update Features set Name = 'Immobilizer' where Name = 'Feature1'");
            migrationBuilder.Sql("update Features set Name = 'Build-in radio' where Name = 'Feature1'");
            migrationBuilder.Sql("update Features set Name = 'ESP' where Name = 'Feature1'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Features set Name = 'Feature1' where Name = 'ABS'");
            migrationBuilder.Sql("update Features set Name = 'Feature2' where Name = 'Air condition'");
            migrationBuilder.Sql("update Features set Name = 'Feature3' where Name = 'Immobilizer'");
            migrationBuilder.Sql("update Features set Name = 'Feature4' where Name = 'Build-in radio'");
            migrationBuilder.Sql("update Features set Name = 'Feature5' where Name = 'ESP'");
        }
    }
}
