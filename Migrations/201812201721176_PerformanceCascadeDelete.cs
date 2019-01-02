namespace OilTeamProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PerformanceCascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Performances", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Performances", "FormID", "dbo.Forms");
            AddForeignKey("dbo.Performances", "EmployeeID", "dbo.Employees", "Id");
            AddForeignKey("dbo.Performances", "FormID", "dbo.Forms", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Performances", "FormID", "dbo.Forms");
            DropForeignKey("dbo.Performances", "EmployeeID", "dbo.Employees");
            AddForeignKey("dbo.Performances", "FormID", "dbo.Forms", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Performances", "EmployeeID", "dbo.Employees", "Id", cascadeDelete: true);
        }
    }
}
