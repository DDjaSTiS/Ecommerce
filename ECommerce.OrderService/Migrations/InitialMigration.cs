using ECommerce.OrderService.Migrations.Migrator;
using ECommerce.OrderService.Models;
using FluentMigrator;

namespace ECommerce.OrderService.Migrations
{
    [Migration(1, "Initial migration")]
    public sealed class InitialMigration : SqlMigration
    {
        protected override string GetUpSql(IServiceProvider services)
        {
            return @"CREATE TABLE ""Order"" (
                        Id SERIAL PRIMARY KEY,
                        CustomerId INT NOT NULL,
                        Region VARCHAR(255) NOT NULL,
                        Status VARCHAR(50) NOT NULL,
                        TotalAmount DECIMAL(10, 2) NOT NULL,
                        OrderDate TIMESTAMP NOT NULL
                    );

                    CREATE TABLE Warehouse (
                        Id SERIAL PRIMARY KEY,
                        Name VARCHAR(255) NOT NULL,
                        Location VARCHAR(255) NOT NULL
                    );

                    CREATE TABLE Region (
                        Id SERIAL PRIMARY KEY,
                        Name VARCHAR(255) NOT NULL,
                        Description TEXT
                    );";

            var t = new List<Test.Id>();
        }

        protected override string GetDownSql(IServiceProvider services)
        {
            return @"
                    drop table ""Order"";
                    drop table ""Warehouse"";
                    drop table ""Region"";";
        }
    }
}
