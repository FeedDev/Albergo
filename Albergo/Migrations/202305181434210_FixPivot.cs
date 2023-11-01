namespace Albergo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixPivot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PivotPrenotazioneServizio", "Quantità", c => c.Int(nullable: false));
            AddColumn("dbo.PivotPrenotazioneServizio", "DataServizio", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Servizio", "Quantità");
            DropColumn("dbo.Servizio", "DataServizio");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Servizio", "DataServizio", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Servizio", "Quantità", c => c.Int(nullable: false));
            DropColumn("dbo.PivotPrenotazioneServizio", "DataServizio");
            DropColumn("dbo.PivotPrenotazioneServizio", "Quantità");
        }
    }
}
