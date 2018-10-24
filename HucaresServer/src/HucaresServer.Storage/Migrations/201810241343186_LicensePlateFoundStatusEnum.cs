namespace HucaresServer.Storage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LicensePlateFoundStatusEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MissingLicensePlates", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.MissingLicensePlates", "LicensePlateFound");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MissingLicensePlates", "LicensePlateFound", c => c.Boolean());
            DropColumn("dbo.MissingLicensePlates", "Status");
        }
    }
}
