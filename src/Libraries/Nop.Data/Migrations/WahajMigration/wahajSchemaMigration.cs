using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Extensions;

namespace Nop.Data.Migrations.WahajMigration;
[NopSchemaMigration("October 18,2024, 11:35 AM", "Nop.Data wahaj base schema", MigrationProcessType.NoMatter)]
internal class wahajSchemaMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.TableFor<CategoryWahaj>();
    }
}
