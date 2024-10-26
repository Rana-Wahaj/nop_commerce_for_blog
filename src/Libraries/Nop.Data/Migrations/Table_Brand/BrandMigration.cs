using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Extensions;

namespace Nop.Data.Migrations.Table_Brand;
[NopSchemaMigration("October 26,2024, 20:32 AM", "New Table Added Brand", MigrationProcessType.NoMatter)]
internal class BrandMigration : Migration
{
    public override void Up()
    {
        Create.TableFor<Brand>();
    }
    public override void Down()
    {
        Delete.Table(nameof(Brand));
    }
}
