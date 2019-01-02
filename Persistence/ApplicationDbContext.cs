using OilTeamProject.Models.Customers;
using OilTeamProject.Models.Employees;
using OilTeamProject.Models.Factories;
using OilTeamProject.Models.Products;
using OilTeamProject.Models.Suppliers;
using System.Data.Entity;

namespace OilTeamProject.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ShiftType> ShiftTypes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<MemberCard> MemberCards { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageStock> PackageStocks { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<RawMaterialStock> RawMaterialStocks { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<OilPress> OilPresses { get; set; }
        public DbSet<Bottling> Bottlings { get; set; }
        public DbSet<UsersAccount> UsersAccounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PersonalDetails> PersonalDetails { get; set; }
        public DbSet<ContactDetails> ContactDetails { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<OrderToSupplier> OrderToSuppliers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PackageOrderToSupplier> PackageOrderToSuppliers { get; set; }

        public ApplicationDbContext() : base("ApplicationDbContext")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Performance>()
                .HasRequired(p => p.Employee)
                .WithMany(e => e.Performances)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Performance>()
                .HasRequired(p => p.Form)
                .WithMany(f => f.Performances)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Works)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shift>()
                .HasMany(e => e.Works)
                .WithRequired(e => e.Shift)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Requests)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Leave>()
                .HasMany(l => l.Requests)
                .WithRequired(l => l.Leave)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Assignments)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Assignments)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);
            // fluent api Products
            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.Sectors)
                .WithRequired(s => s.Warehouse)
                .HasForeignKey(s => s.WarehouseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RawMaterialStock>()
                .HasRequired(rs => rs.Sector);

            modelBuilder.Entity<RawMaterialStock>()
                .HasRequired(rs => rs.RawMaterial);

            modelBuilder.Entity<PackageStock>()
                .HasRequired(ps => ps.Sector);

            modelBuilder.Entity<PackageStock>()
                .HasRequired(ps => ps.Package);

            modelBuilder.Entity<ProductStock>()
                .HasRequired(ps => ps.Sector);

            modelBuilder.Entity<RawMaterial>()
                .HasMany(r => r.Categories)
                .WithRequired(c => c.RawMaterial)
                .HasForeignKey(c => c.RawMaterialID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithRequired(p => p.Category)
                .HasForeignKey(p => p.CategoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Package);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductStocks)
                .WithRequired(ps => ps.Product)
                .HasForeignKey(ps => ps.ProductID);

            modelBuilder.Entity<ProductStock>()
                .HasRequired(ps => ps.Bottling)
                .WithMany(b => b.ProductStocks)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderToSupplier>()
                .HasMany(p => p.Packages);


            modelBuilder.Entity<Package>()
                .HasMany(o => o.OrderToSuppliers);

            base.OnModelCreating(modelBuilder);
        }
    }
}