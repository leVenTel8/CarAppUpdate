namespace CarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        YearMade = c.Int(nullable: false),
                        GosNumber = c.String(),
                        DateRegistration = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Inspections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateInspection = c.String(),
                        NumberInspection = c.Int(nullable: false),
                        CarId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId)
                .Index(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inspections", "CarId", "dbo.Cars");
            DropIndex("dbo.Inspections", new[] { "CarId" });
            DropTable("dbo.Inspections");
            DropTable("dbo.Cars");
        }
    }
}
