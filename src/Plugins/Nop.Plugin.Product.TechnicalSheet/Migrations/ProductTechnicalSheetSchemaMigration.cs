using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Product.TechnicalSheet.Domain;

namespace Nop.Plugin.Product.TechnicalSheet.Migrations;

    [NopMigration("2026/05/25 12:00:00", "Nop.Plugin.Product.TechnicalSheet schema", MigrationProcessType.Installation)]
    public class ProductTechnicalSheetSchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
        Create.TableFor<ProductTechnicalSheet>();

        Create.ForeignKey()
                .FromTable("ProductTechnicalSheet").ForeignColumn("ProductId")
                .ToTable("Product").PrimaryColumn("Id");
        }
    }

