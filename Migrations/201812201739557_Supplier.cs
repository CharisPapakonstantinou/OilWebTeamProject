namespace OilTeamProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Supplier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderToSuppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        RawMaterialId = c.Int(),
                        PackageId = c.Int(),
                        Quantity = c.Int(nullable: false),
                        OrderHasBeenReceived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .ForeignKey("dbo.RawMaterials", t => t.RawMaterialId)
                .Index(t => t.SupplierId)
                .Index(t => t.RawMaterialId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        SupplyingMatetrial = c.Int(nullable: false),
                        OliveSupplying_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RawMaterials", t => t.OliveSupplying_ID)
                .Index(t => t.OliveSupplying_ID);
            
            CreateTable(
                "dbo.PackageOrderToSuppliers",
                c => new
                    {
                        OrderToSupplierId = c.Int(nullable: false),
                        PackageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderToSupplierId, t.PackageId })
                .ForeignKey("dbo.OrderToSuppliers", t => t.OrderToSupplierId, cascadeDelete: true)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.OrderToSupplierId)
                .Index(t => t.PackageId);
            
            CreateTable(
                "dbo.SupplierPackages",
                c => new
                    {
                        Supplier_Id = c.Int(nullable: false),
                        Package_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Supplier_Id, t.Package_ID })
                .ForeignKey("dbo.Suppliers", t => t.Supplier_Id, cascadeDelete: true)
                .ForeignKey("dbo.Packages", t => t.Package_ID, cascadeDelete: true)
                .Index(t => t.Supplier_Id)
                .Index(t => t.Package_ID);
            
            CreateTable(
                "dbo.OrderToSupplierPackages",
                c => new
                    {
                        OrderToSupplier_Id = c.Int(nullable: false),
                        Package_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderToSupplier_Id, t.Package_ID })
                .ForeignKey("dbo.OrderToSuppliers", t => t.OrderToSupplier_Id, cascadeDelete: true)
                .ForeignKey("dbo.Packages", t => t.Package_ID, cascadeDelete: true)
                .Index(t => t.OrderToSupplier_Id)
                .Index(t => t.Package_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PackageOrderToSuppliers", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.PackageOrderToSuppliers", "OrderToSupplierId", "dbo.OrderToSuppliers");
            DropForeignKey("dbo.OrderToSuppliers", "RawMaterialId", "dbo.RawMaterials");
            DropForeignKey("dbo.OrderToSupplierPackages", "Package_ID", "dbo.Packages");
            DropForeignKey("dbo.OrderToSupplierPackages", "OrderToSupplier_Id", "dbo.OrderToSuppliers");
            DropForeignKey("dbo.SupplierPackages", "Package_ID", "dbo.Packages");
            DropForeignKey("dbo.SupplierPackages", "Supplier_Id", "dbo.Suppliers");
            DropForeignKey("dbo.OrderToSuppliers", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Suppliers", "OliveSupplying_ID", "dbo.RawMaterials");
            DropIndex("dbo.OrderToSupplierPackages", new[] { "Package_ID" });
            DropIndex("dbo.OrderToSupplierPackages", new[] { "OrderToSupplier_Id" });
            DropIndex("dbo.SupplierPackages", new[] { "Package_ID" });
            DropIndex("dbo.SupplierPackages", new[] { "Supplier_Id" });
            DropIndex("dbo.PackageOrderToSuppliers", new[] { "PackageId" });
            DropIndex("dbo.PackageOrderToSuppliers", new[] { "OrderToSupplierId" });
            DropIndex("dbo.Suppliers", new[] { "OliveSupplying_ID" });
            DropIndex("dbo.OrderToSuppliers", new[] { "RawMaterialId" });
            DropIndex("dbo.OrderToSuppliers", new[] { "SupplierId" });
            DropTable("dbo.OrderToSupplierPackages");
            DropTable("dbo.SupplierPackages");
            DropTable("dbo.PackageOrderToSuppliers");
            DropTable("dbo.Suppliers");
            DropTable("dbo.OrderToSuppliers");
        }
    }
}
