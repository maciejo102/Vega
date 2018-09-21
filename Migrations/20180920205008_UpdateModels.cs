using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vega.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Models set Name = 'Escort' where Name = 'Make1-ModelA'");
            migrationBuilder.Sql("update Models set Name = 'S-Max' where Name = 'Make1-ModelB'");
            migrationBuilder.Sql("update Models set Name = 'Galaxy' where Name = 'Make1-ModelC'");
            migrationBuilder.Sql("update Models set Name = 'Megane' where Name = 'Make2-ModelA'");
            migrationBuilder.Sql("update Models set Name = 'Laguna' where Name = 'Make2-ModelB'");
            migrationBuilder.Sql("update Models set Name = 'Clio' where Name = 'Make2-ModelC'");
            migrationBuilder.Sql("update Models set Name = 'Octavia' where Name = 'Make2-ModelA'");
            migrationBuilder.Sql("update Models set Name = 'Superb' where Name = 'Make2-ModelB'");
            migrationBuilder.Sql("update Models set Name = 'Kodiaq' where Name = 'Make2-ModelC'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Models set Name = 'Make1-ModelA' where Name = 'Escort'");
            migrationBuilder.Sql("update Models set Name = 'Make1-ModelB' where Name = 'S-Max'");
            migrationBuilder.Sql("update Models set Name = 'Make1-ModelC' where Name = 'Galaxy'");
            migrationBuilder.Sql("update Models set Name = 'Make2-ModelA' where Name = 'Megane'");
            migrationBuilder.Sql("update Models set Name = 'Make2-ModelB' where Name = 'Laguna'");
            migrationBuilder.Sql("update Models set Name = 'Make2-ModelC' where Name = 'Clio'");
            migrationBuilder.Sql("update Models set Name = 'Make2-ModelA' where Name = 'Octavia'");
            migrationBuilder.Sql("update Models set Name = 'Make2-ModelB' where Name = 'Superb'");
            migrationBuilder.Sql("update Models set Name = 'Make2-ModelC' where Name = 'Kodiaq'");
        }
    }
}
