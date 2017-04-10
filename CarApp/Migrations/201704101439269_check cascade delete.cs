namespace CarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkcascadedelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Inspections", "CarId", "dbo.Cars");
            AddColumn("dbo.Inspections", "Car_Id", c => c.Int());
            CreateIndex("dbo.Inspections", "Car_Id");
            AddForeignKey("dbo.Inspections", "Car_Id", "dbo.Cars", "Id");
            AddForeignKey("dbo.Inspections", "CarId", "dbo.Cars", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inspections", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Inspections", "Car_Id", "dbo.Cars");
            DropIndex("dbo.Inspections", new[] { "Car_Id" });
            DropColumn("dbo.Inspections", "Car_Id");
            AddForeignKey("dbo.Inspections", "CarId", "dbo.Cars", "Id");
        }
    }
}
