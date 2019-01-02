namespace OilTeamProject.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ShiftTypesDepRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ShiftTypes(Id, Name) VALUES ( 1,'Morning')");
            Sql("INSERT INTO ShiftTypes(Id, Name) VALUES ( 2,'Evening')");
            Sql("INSERT INTO ShiftTypes(Id, Name) VALUES ( 3,'Night')");

            Sql("INSERT INTO Departments( Name, Address) VALUES ( 'HR', 'Palaiologou 1')");
            Sql("INSERT INTO Departments( Name, Address) VALUES ( 'Sales', 'Spiridwn 3')");
            Sql("INSERT INTO Departments( Name, Address) VALUES ( 'Production', 'Panepistimiou 8')");

            Sql("INSERT INTO Roles( Name) VALUES ( 'Administrator')");
            Sql("INSERT INTO Roles( Name) VALUES ( 'Manager')");
            Sql("INSERT INTO Roles( Name) VALUES ( 'Worker')");
        }

        public override void Down()
        {
            Sql("DELETE FROM ShiftTypes WHERE Name IN ('Morning', 'Evening', 'Night')");
            Sql("DELETE FROM Departments WHERE Name IN ('HR', 'Sales', 'Production')");
            Sql("DELETE FROM Roles WHERE Name IN ('Administrator', 'Manager', 'Worker')");
        }
    }
}
