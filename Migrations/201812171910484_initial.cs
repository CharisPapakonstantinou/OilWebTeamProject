namespace OilTeamProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        QuestionID = c.Int(nullable: false),
                        PerformanceID = c.Int(nullable: false),
                        Text = c.String(),
                        QuestionAnswer = c.Int(),
                    })
                .PrimaryKey(t => new { t.QuestionID, t.PerformanceID })
                .ForeignKey("dbo.Performances", t => t.PerformanceID, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID)
                .Index(t => t.PerformanceID);
            
            CreateTable(
                "dbo.Performances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        EvaluationID = c.Int(nullable: false),
                        FormID = c.Int(nullable: false),
                        OveralRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateEvaluated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Evaluations", t => t.EvaluationID, cascadeDelete: true)
                .ForeignKey("dbo.Forms", t => t.FormID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.EvaluationID)
                .Index(t => t.FormID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        Salary = c.Double(nullable: false),
                        RemaingDaysOfLeave = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.ProjectId })
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DueDate = c.DateTime(nullable: false),
                        StartingDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactDetails",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        MobilePhone = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        PostalCode = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonalDetails",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        CurrentFamilyStatus = c.Int(nullable: false),
                        NumberOfChildren = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Sex = c.Int(nullable: false),
                        SSN = c.String(),
                        IdentityCard = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        LeaveID = c.Int(nullable: false),
                        DateRequestedLeave = c.DateTime(nullable: false),
                        IsAccepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeID, t.LeaveID })
                .ForeignKey("dbo.Leaves", t => t.LeaveID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .Index(t => t.EmployeeID)
                .Index(t => t.LeaveID);
            
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartDateOfLeave = c.DateTime(nullable: false),
                        EndDateOfLeave = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        ShiftId = c.Int(nullable: false),
                        IsCanceled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeID, t.ShiftId })
                .ForeignKey("dbo.Shifts", t => t.ShiftId)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .Index(t => t.EmployeeID)
                .Index(t => t.ShiftId);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        ShiftTypeId = c.Byte(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.ShiftTypes", t => t.ShiftTypeId, cascadeDelete: true)
                .Index(t => t.ShiftTypeId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.ShiftTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Evaluations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartEvaluationDate = c.DateTime(nullable: false),
                        EndEvaluationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Theme = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Bottlings",
                c => new
                    {
                        BottlingID = c.Int(nullable: false, identity: true),
                        BottlingDate = c.DateTime(nullable: false),
                        ProductCode = c.String(),
                        BottlingLotNumber = c.Int(nullable: false),
                        Tank = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.BottlingID);
            
            CreateTable(
                "dbo.Factories",
                c => new
                    {
                        FactoryID = c.Int(nullable: false, identity: true),
                        FactoryName = c.String(),
                        FactoryAdress = c.String(),
                        FactoryPhoneNumber = c.String(),
                        Owner = c.String(),
                        Bottling_BottlingID = c.Int(),
                    })
                .PrimaryKey(t => t.FactoryID)
                .ForeignKey("dbo.Bottlings", t => t.Bottling_BottlingID)
                .Index(t => t.Bottling_BottlingID);
            
            CreateTable(
                "dbo.OilPresses",
                c => new
                    {
                        OilPressID = c.Int(nullable: false, identity: true),
                        OilPressName = c.String(),
                        OliveType = c.String(),
                        OlivesWeight = c.Double(nullable: false),
                        OilOutput = c.Double(nullable: false),
                        ProductionDate = c.DateTime(nullable: false),
                        FactoryID = c.Int(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.OilPressID)
                .ForeignKey("dbo.Factories", t => t.FactoryID)
                .ForeignKey("dbo.UsersAccounts", t => t.UserId)
                .Index(t => t.FactoryID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UsersAccounts",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ProductStocks",
                c => new
                    {
                        ProductStockID = c.String(nullable: false, maxLength: 128),
                        AvailableQuantity = c.Int(nullable: false),
                        ActualQuantity = c.Int(nullable: false),
                        SectorID = c.Int(nullable: false),
                        BottlingID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductStockID)
                .ForeignKey("dbo.Bottlings", t => t.BottlingID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Sectors", t => t.SectorID, cascadeDelete: true)
                .Index(t => t.SectorID)
                .Index(t => t.BottlingID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        OrderProductsID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ProductStockID = c.String(maxLength: 128),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderProductsID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.ProductStocks", t => t.ProductStockID)
                .Index(t => t.OrderID)
                .Index(t => t.ProductStockID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        PaidOff = c.Boolean(nullable: false),
                        PaymentDate = c.DateTime(),
                        TotalCost = c.Double(nullable: false),
                        PaymentType = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        ActivityStatus = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        CompanyName = c.String(maxLength: 50),
                        CompanyType = c.Int(),
                        Gender = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Country = c.String(nullable: false, maxLength: 30),
                        City = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 40),
                        PostalCode = c.Int(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        CustomerID = c.Int(nullable: false),
                        CreditCardNumber = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.Customers", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.MemberCards",
                c => new
                    {
                        CustomerID = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        MemberCardCode = c.String(),
                        Credits = c.Int(nullable: false),
                        NewsLetter = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.Customers", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        Slug = c.String(),
                        Featured = c.Boolean(nullable: false),
                        Discount = c.Int(nullable: false),
                        BarCode = c.String(),
                        Thumbnail = c.String(),
                        PackageID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.Packages", t => t.PackageID, cascadeDelete: true)
                .Index(t => t.PackageID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Slug = c.String(),
                        Price = c.Double(nullable: false),
                        Thumbnail = c.String(),
                        RawMaterialID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.RawMaterials", t => t.RawMaterialID)
                .Index(t => t.RawMaterialID);
            
            CreateTable(
                "dbo.RawMaterials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Material = c.Int(nullable: false),
                        Size = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                        Length = c.Double(nullable: false),
                        Width = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sectors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StoredType = c.String(),
                        WarehouseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseID)
                .Index(t => t.WarehouseID);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sender = c.String(),
                        Date = c.DateTime(nullable: false),
                        Email = c.String(),
                        Mobile = c.String(),
                        Content = c.String(),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PackageStocks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        PackageID = c.Int(nullable: false),
                        SectorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Packages", t => t.PackageID, cascadeDelete: true)
                .ForeignKey("dbo.Sectors", t => t.SectorID, cascadeDelete: true)
                .Index(t => t.PackageID, unique: true)
                .Index(t => t.SectorID);
            
            CreateTable(
                "dbo.RawMaterialStocks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        RawMaterialID = c.Int(nullable: false),
                        SectorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RawMaterials", t => t.RawMaterialID, cascadeDelete: true)
                .ForeignKey("dbo.Sectors", t => t.SectorID, cascadeDelete: true)
                .Index(t => t.RawMaterialID, unique: true)
                .Index(t => t.SectorID);
            
            CreateTable(
                "dbo.QuestionForms",
                c => new
                    {
                        Question_ID = c.Int(nullable: false),
                        Form_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Question_ID, t.Form_ID })
                .ForeignKey("dbo.Questions", t => t.Question_ID, cascadeDelete: true)
                .ForeignKey("dbo.Forms", t => t.Form_ID, cascadeDelete: true)
                .Index(t => t.Question_ID)
                .Index(t => t.Form_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RawMaterialStocks", "SectorID", "dbo.Sectors");
            DropForeignKey("dbo.RawMaterialStocks", "RawMaterialID", "dbo.RawMaterials");
            DropForeignKey("dbo.PackageStocks", "SectorID", "dbo.Sectors");
            DropForeignKey("dbo.PackageStocks", "PackageID", "dbo.Packages");
            DropForeignKey("dbo.Images", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductStocks", "SectorID", "dbo.Sectors");
            DropForeignKey("dbo.Sectors", "WarehouseID", "dbo.Warehouses");
            DropForeignKey("dbo.ProductStocks", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "PackageID", "dbo.Packages");
            DropForeignKey("dbo.Categories", "RawMaterialID", "dbo.RawMaterials");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.OrderProducts", "ProductStockID", "dbo.ProductStocks");
            DropForeignKey("dbo.OrderProducts", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.MemberCards", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.CreditCards", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.ProductStocks", "BottlingID", "dbo.Bottlings");
            DropForeignKey("dbo.Factories", "Bottling_BottlingID", "dbo.Bottlings");
            DropForeignKey("dbo.OilPresses", "UserId", "dbo.UsersAccounts");
            DropForeignKey("dbo.OilPresses", "FactoryID", "dbo.Factories");
            DropForeignKey("dbo.Answers", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Answers", "PerformanceID", "dbo.Performances");
            DropForeignKey("dbo.QuestionForms", "Form_ID", "dbo.Forms");
            DropForeignKey("dbo.QuestionForms", "Question_ID", "dbo.Questions");
            DropForeignKey("dbo.Performances", "FormID", "dbo.Forms");
            DropForeignKey("dbo.Performances", "EvaluationID", "dbo.Evaluations");
            DropForeignKey("dbo.Performances", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Works", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Works", "ShiftId", "dbo.Shifts");
            DropForeignKey("dbo.Shifts", "ShiftTypeId", "dbo.ShiftTypes");
            DropForeignKey("dbo.Shifts", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Employees", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Requests", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Requests", "LeaveID", "dbo.Leaves");
            DropForeignKey("dbo.PersonalDetails", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ContactDetails", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Assignments", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Assignments", "ProjectId", "dbo.Projects");
            DropIndex("dbo.QuestionForms", new[] { "Form_ID" });
            DropIndex("dbo.QuestionForms", new[] { "Question_ID" });
            DropIndex("dbo.RawMaterialStocks", new[] { "SectorID" });
            DropIndex("dbo.RawMaterialStocks", new[] { "RawMaterialID" });
            DropIndex("dbo.PackageStocks", new[] { "SectorID" });
            DropIndex("dbo.PackageStocks", new[] { "PackageID" });
            DropIndex("dbo.Images", new[] { "ProductID" });
            DropIndex("dbo.Sectors", new[] { "WarehouseID" });
            DropIndex("dbo.Categories", new[] { "RawMaterialID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "PackageID" });
            DropIndex("dbo.MemberCards", new[] { "CustomerID" });
            DropIndex("dbo.CreditCards", new[] { "CustomerID" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderProducts", new[] { "ProductStockID" });
            DropIndex("dbo.OrderProducts", new[] { "OrderID" });
            DropIndex("dbo.ProductStocks", new[] { "ProductID" });
            DropIndex("dbo.ProductStocks", new[] { "BottlingID" });
            DropIndex("dbo.ProductStocks", new[] { "SectorID" });
            DropIndex("dbo.OilPresses", new[] { "UserId" });
            DropIndex("dbo.OilPresses", new[] { "FactoryID" });
            DropIndex("dbo.Factories", new[] { "Bottling_BottlingID" });
            DropIndex("dbo.Shifts", new[] { "DepartmentId" });
            DropIndex("dbo.Shifts", new[] { "ShiftTypeId" });
            DropIndex("dbo.Works", new[] { "ShiftId" });
            DropIndex("dbo.Works", new[] { "EmployeeID" });
            DropIndex("dbo.Requests", new[] { "LeaveID" });
            DropIndex("dbo.Requests", new[] { "EmployeeID" });
            DropIndex("dbo.PersonalDetails", new[] { "EmployeeId" });
            DropIndex("dbo.ContactDetails", new[] { "EmployeeId" });
            DropIndex("dbo.Assignments", new[] { "ProjectId" });
            DropIndex("dbo.Assignments", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "RoleId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Performances", new[] { "FormID" });
            DropIndex("dbo.Performances", new[] { "EvaluationID" });
            DropIndex("dbo.Performances", new[] { "EmployeeID" });
            DropIndex("dbo.Answers", new[] { "PerformanceID" });
            DropIndex("dbo.Answers", new[] { "QuestionID" });
            DropTable("dbo.QuestionForms");
            DropTable("dbo.RawMaterialStocks");
            DropTable("dbo.PackageStocks");
            DropTable("dbo.Messages");
            DropTable("dbo.Images");
            DropTable("dbo.Warehouses");
            DropTable("dbo.Sectors");
            DropTable("dbo.Packages");
            DropTable("dbo.RawMaterials");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.MemberCards");
            DropTable("dbo.CreditCards");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.ProductStocks");
            DropTable("dbo.UsersAccounts");
            DropTable("dbo.OilPresses");
            DropTable("dbo.Factories");
            DropTable("dbo.Bottlings");
            DropTable("dbo.Questions");
            DropTable("dbo.Forms");
            DropTable("dbo.Evaluations");
            DropTable("dbo.ShiftTypes");
            DropTable("dbo.Shifts");
            DropTable("dbo.Works");
            DropTable("dbo.Roles");
            DropTable("dbo.Leaves");
            DropTable("dbo.Requests");
            DropTable("dbo.PersonalDetails");
            DropTable("dbo.Departments");
            DropTable("dbo.ContactDetails");
            DropTable("dbo.Projects");
            DropTable("dbo.Assignments");
            DropTable("dbo.Employees");
            DropTable("dbo.Performances");
            DropTable("dbo.Answers");
        }
    }
}
