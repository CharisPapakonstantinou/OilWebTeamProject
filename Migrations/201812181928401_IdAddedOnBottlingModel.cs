namespace OilTeamProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdAddedOnBottlingModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bottlings", "FactoryID", c => c.Int());
            AddColumn("dbo.Bottlings", "Factory_FactoryID", c => c.Int());
            CreateIndex("dbo.Bottlings", "Factory_FactoryID");
            AddForeignKey("dbo.Bottlings", "Factory_FactoryID", "dbo.Factories", "FactoryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bottlings", "Factory_FactoryID", "dbo.Factories");
            DropIndex("dbo.Bottlings", new[] { "Factory_FactoryID" });
            DropColumn("dbo.Bottlings", "Factory_FactoryID");
            DropColumn("dbo.Bottlings", "FactoryID");
        }
    }
}
