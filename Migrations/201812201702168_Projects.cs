namespace OilTeamProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Projects : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "DateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "DepartmentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Projects", "StartingDate", c => c.DateTime());
            AlterColumn("dbo.Projects", "EndDate", c => c.DateTime());
            CreateIndex("dbo.Projects", "DepartmentId");
            AddForeignKey("dbo.Projects", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Projects", new[] { "DepartmentId" });
            AlterColumn("dbo.Projects", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Projects", "StartingDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Projects", "DepartmentId");
            DropColumn("dbo.Assignments", "DateTime");
        }
    }
}
