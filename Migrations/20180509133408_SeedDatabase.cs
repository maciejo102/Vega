using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vega.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Makes (Name) values ('Make1')");
            migrationBuilder.Sql("insert into Makes (Name) values ('Make2')");
            migrationBuilder.Sql("insert into Makes (Name) values ('Make3')");

            // hardcoded MakeId is bad, because, in case of downgrade, new Make Id will be 4, 5, 6 and so on
            // migrationBuilder.Sql(" insert into Models (Name, MakeId) values ('Make1-ModelA', 1)");

            migrationBuilder.Sql(" insert into Models (Name, MakeId) values ('Make1-ModelB', (select Id from Makes where Name = 'Make1'))");
            migrationBuilder.Sql(" insert into Models (Name, MakeId) values ('Make1-ModelB', (select Id from Makes where Name = 'Make1'))");
            migrationBuilder.Sql(" insert into Models (Name, MakeId) values ('Make1-ModelC', (select Id from Makes where Name = 'Make1'))");

            migrationBuilder.Sql(" insert into Models (Name, MakeId) values ('Make1-ModelA', (select Id from Makes where Name = 'Make2'))");
            migrationBuilder.Sql(" insert into Models (Name, MakeId) values ('Make1-ModelB', (select Id from Makes where Name = 'Make2'))");
            migrationBuilder.Sql(" insert into Models (Name, MakeId) values ('Make1-ModelC', (select Id from Makes where Name = 'Make2'))");

            migrationBuilder.Sql(" insert into Models (Name, MakeId) values ('Make1-ModelA', (select Id from Makes where Name = 'Make3'))");
            migrationBuilder.Sql(" insert into Models (Name, MakeId) values ('Make1-ModelB', (select Id from Makes where Name = 'Make3'))");
            migrationBuilder.Sql(" insert into Models (Name, MakeId) values ('Make1-ModelC', (select Id from Makes where Name = 'Make3'))");
        }

        // downgrade the database
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // usuwanie kaskadowe? mode
            migrationBuilder.Sql("delete from Makes where Name in ('Make1', 'Make2', 'Make3')");
        }
    }
}
